using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public string id;
    public GameObject bubble;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI bubbleText;

    public GameObject Portrait;
    public GameObject PortraitShooting;

    List<string> lines;

    void Start() {
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
        string randomLine = lines[Random.Range(0, lines.Count)];
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
        }
        else {
            // click sound
        }
        yield return new WaitForSeconds(1);
        PortraitShooting.SetActive(false);
        Portrait.SetActive(true);
        callback();
    }
}
