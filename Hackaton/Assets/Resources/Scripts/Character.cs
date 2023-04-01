using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public string id;
    public GameObject bubble;
    public TextMeshProUGUI bubbleText;

    List<string> lines;

    void Start() {
        LinesLoader linesLoader = FindObjectOfType<LinesLoader>();
        lines = linesLoader.GetLines(id);
    }

    public void ShowBubble() {
        if (lines.Count == 0) {
            return;
        }
        string randomLine = lines[Random.Range(0, lines.Count)];
        lines.Remove(randomLine);
        bubbleText.text = randomLine;
        bubble.SetActive(true);
    }

    public void HideBubble() {
        bubble.SetActive(false);
    }

    public void TakeATurn() {
    }
}
