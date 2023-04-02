using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public string id;
    public GameObject bubble;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI bubbleText;

    public GameObject Portrait;
    public GameObject PortraitShooting;
    public Image splash;

    List<string> lines;
    Game game;

    void Start() {
        game = FindObjectOfType<Game>();
        Init();
    }

    void Init() {
        LinesLoader linesLoader = FindObjectOfType<LinesLoader>();
        nameText.text = linesLoader.GetName(id);
        lines = linesLoader.GetLines(id);
    }

    public void ShowBubble() {
        if (lines.Count == 0) {
            Init();
        }
        string randomLine = lines[0];
        lines.Remove(randomLine);
        bubbleText.text = randomLine;
        bubble.SetActive(true);
    }

    public void HideBubble() {
        bubble.SetActive(false);
    }

    public IEnumerator TakeATurn(bool lose, System.Action callback) {
        Portrait.SetActive(false);
        PortraitShooting.SetActive(true);
        yield return new WaitForSeconds(1);
        Debug.Log("enemy shoot");
        if (lose) {
            // splash, shoot sound, etc
            splash.color = game.GetColor();
            splash.gameObject.SetActive(true);
            Sound.Play("fail");
        }
        else {
            // click sound
            Sound.Play("none");
        }
        yield return new WaitForSeconds(1);
        PortraitShooting.SetActive(false);
        Portrait.SetActive(true);
        callback();
    }

    public void Hide() {
        splash.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
