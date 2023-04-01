using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public Transform gameTransform;
    public HUDLogic hudLogic;

    enum Turn { None, Player, Enemy };
    Turn turn;

    public GameObject wheel;
    public GameObject trigger;
    
    int score = 100;
    public TextMeshProUGUI scoreText;
    
    int bulletSlot = 5;
    public List<GameObject> enemies;
    public GameObject boss;
    List<GameObject> currentEnemies;
    Character enemy;
    int level = 0;

    public void Start() {
        currentEnemies = new List<GameObject>(enemies);
        AddScore(0);
        bulletSlot = Random.Range(3, 6);
        PickEnemy();
        wheel.GetComponent<Wheel>().Subscribe(() => {
            wheel.SetActive(false);
            trigger.SetActive(true);
            SetTurnPlayer();
        });
    }

    public void PickEnemy() {
        if (enemies.Count == 0) {
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
        enemy.HideBubble();
        // enemy shooting animation
        if (bulletSlot == 0) {
            Win();
            return;
        }
        bulletSlot--;
        SetTurnPlayer();
    }

    void AddScore(int add) {
        score += add;
        scoreText.text = score.ToString();
    }

    void Win() {
        Debug.Log("win");
        hudLogic.YouWin();
    }

    void Lose() {
        Debug.Log("lose");
        hudLogic.YouFail();
    }

}
