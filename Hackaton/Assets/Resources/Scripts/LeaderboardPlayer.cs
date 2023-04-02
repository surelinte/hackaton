using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardPlayer : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _score;
    public TextMeshProUGUI _place;
    public void Init(string name, int score, int place) {
        _name.text = name;
        _score.text = score.ToString();
        _place.text = place.ToString();
    }
}
