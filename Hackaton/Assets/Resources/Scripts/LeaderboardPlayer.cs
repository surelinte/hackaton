using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardPlayer : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public void Init(string name, int score) {
        tmpro.text = name + ": " + score.ToString() + " Rixcoins";
    }
}
