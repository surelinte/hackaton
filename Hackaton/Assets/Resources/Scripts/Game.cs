using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    const int ENEMIES_COUNT = 7;

    public Transform gameTransform;
    public HUDLogic hudLogic;

    enum Turn { None, Player, Enemy };
    Turn turn;

    public GameObject wheel;
    public GameObject trigger;
    public Image bullet;
    public ParticleSystem splash;
    
    int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winScoreText;
    
    int bulletSlot = 5;
    public List<GameObject> enemies;
    List<GameObject> passedEnemies = new List<GameObject>();

    Character enemy;
    int level = 0;

    public int[] scores = {100, 300, 500, 1000, 2000, 5000, 10000};
    public float[] chances = {90, 80, 70, 60, 50, 40, 30};

    int[] randomOrder = new int[ENEMIES_COUNT];
    
    public Color[] colors;
    Color color;

    void Shuffle(List<GameObject> list) {
        for (int currentIndex = list.Count - 1; currentIndex >= 0; currentIndex -= 1) {
            int randomIndex = Mathf.FloorToInt(Random.value * currentIndex);
            GameObject temporaryValue = list[currentIndex];
            list[currentIndex] = list[randomIndex];
            list[randomIndex] = temporaryValue;
        }
    }

    public void Start() {
        Shuffle(enemies);
        AddScore(0);
        Roll();
        PickEnemy();
        NextColor();
        wheel.GetComponent<Wheel>().Subscribe(() => {
            wheel.SetActive(false);
            trigger.SetActive(true);
            SetTurnPlayer();
        });
    }

    public void PickEnemy() {
        if (enemies.Count == 0) {
            Shuffle(passedEnemies);
            enemies.AddRange(passedEnemies);
            passedEnemies.Clear();
        }
        GameObject randomEnemy = enemies[0];
        enemies.Remove(randomEnemy);
        passedEnemies.Add(randomEnemy);
        SetEnemy(randomEnemy);
    }

    void SetEnemy(GameObject gameObj) {
        if (enemy != null) {
            enemy.gameObject.SetActive(false);
        }
        gameObj.SetActive(true);
        enemy = gameObj.GetComponent<Character>();
    }

    public void Roll() {
        float rnd = 100 * Random.value;
        bool win = rnd < chances[level];
        bulletSlot = 2 * Random.Range(0, 3) + (win ? 1 : 0);
        Debug.Log("random roll " + rnd + " " + chances[level] + " " + bulletSlot);
    }

    public void Shoot() {
        if (turn != Turn.Player) {
            return;
        }
        if (bulletSlot == 0) {
            Lose();
            return;
        }
        bulletSlot--;
        SetTurnEnemy();
    }

    void SetTurnPlayer() {
        Debug.Log("player turn, " + bulletSlot.ToString());
        enemy.ShowBubble();
        turn = Turn.Player;
    }

    void SetTurnEnemy() {
        Debug.Log("enemy turn, " + bulletSlot.ToString());
        turn = Turn.Enemy;
        enemy.HideBubble();
        StartCoroutine(enemy.TakeATurn(bulletSlot == 0, () => {
            if (bulletSlot == 0) {
                Win();
                return;
            }
            bulletSlot--;
            SetTurnPlayer();
        }));
    }

    void AddScore(int add) {
        score += add;
        scoreText.text = score.ToString();
        winScoreText.text = "Рикскоины: " + score.ToString();
    }

    public int GetScore() {
        return score;
    }

    void Win() {
        Debug.Log("win");
        int addScore = Mathf.RoundToInt(Random.Range(0.9f * scores[level], 1.1f * scores[level]));
        AddScore(addScore);
        if (level == ENEMIES_COUNT - 1) {
            hudLogic.TakeMoney();
        }
        else {
            hudLogic.YouWin();
        }
    }

    void Lose() {
        Debug.Log("lose");
        AddScore(-score);
        hudLogic.YouFail();
    }

    public void StartAgain() {
        level = 0;
        AddScore(-score);
        wheel.SetActive(true);
        trigger.SetActive(false);
        Shuffle(passedEnemies);
        enemies.AddRange(passedEnemies);
        passedEnemies.Clear();
        Roll();
        NextColor();
        PickEnemy();
    }

    public void NextLevel() {
        level++;
        wheel.SetActive(true);
        trigger.SetActive(false);
        Roll();
        NextColor();
        PickEnemy();
    }

    public void NextColor() {
        color = colors[Random.Range(0, colors.Length)];
        bullet.color = color;
        var main = splash.main;
        main.startColor = color;
    }
}
