using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Transform gameTransform;
    public HUDLogic hudLogic;

    enum Turn { None, Player, Enemy };
    Turn turn;

    public GameObject wheel;
    public GameObject trigger;
    public Image bullet;
    
    int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winScoreText;
    
    int bulletSlot = 5;
    public List<GameObject> enemies;
    public GameObject boss;
    List<GameObject> currentEnemies;
    Character enemy;
    int level = 0;

    public int[] scores = {100, 300, 500, 1000, 2000, 5000};
    public float[] chances = {90, 80, 70, 60, 50, 40};
    
    public Color[] colors;
    Color color;

    public void Start() {
        currentEnemies = new List<GameObject>(enemies);
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
        if (currentEnemies.Count == 0) {
            if (level == enemies.Count) {
                SetEnemy(boss);
                return;
            }
            currentEnemies = new List<GameObject>(enemies);
        }
        GameObject randomEnemy = currentEnemies[Random.Range(0, currentEnemies.Count)];
        currentEnemies.Remove(randomEnemy);
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
        if (enemy.gameObject == boss) {
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
        wheel.SetActive(true);
        trigger.SetActive(false);
        Roll();
        PickEnemy();
    }

    public void NextLevel() {
        level++;
        wheel.SetActive(true);
        trigger.SetActive(false);
        Roll();
        PickEnemy();
    }

    public void NextColor() {
        color = colors[Random.Range(0, colors.Length)];
        bullet.color = color;
    }
}
