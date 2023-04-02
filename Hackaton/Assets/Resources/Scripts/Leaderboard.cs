using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    public Game game;
    public bool localServer = false;

    public TMP_InputField inputName;
    
    public GameObject content;
    public GameObject entryPrefab;

    [System.Serializable]
    public class LeaderboardPlayerData {
        public string name;
        public int score;
    }
    [System.Serializable]
    public class LeaderboardData {
        public LeaderboardPlayerData[] players;
    }

    public void OnEnable() {
        GetScore();
    }

    private string GetUrl() {
        return localServer ? "http://localhost:3000/" : "https://gamedeva-maria.onrender.com/";
    }

    public void SendScore() {
        SendScore(inputName.text, game.GetScore());
    }

    public void SendScore(string name, int score) {
        StartCoroutine(GetRequest(GetUrl() + "send/gamedeva/" + name + "/" + score.ToString()));
    }

    public void GetScore() {
        StartCoroutine(GetRequest(GetUrl() + "get/gamedeva"));
    }

	IEnumerator GetRequest(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
            LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(www.downloadHandler.text);
            foreach (Transform child in content.transform) {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = 0; i < leaderboardData.players.Length; i++) {
                LeaderboardPlayerData player = leaderboardData.players[i];
                GameObject leaderboardPlayer = Instantiate(entryPrefab, content.transform);
                leaderboardPlayer.GetComponent<LeaderboardPlayer>().Init(player.name, player.score, i + 1);
            }
        }
    }
}
