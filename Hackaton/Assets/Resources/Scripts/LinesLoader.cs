using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinesLoader : MonoBehaviour
{
    public TextAsset linesJson;

    [System.Serializable]
    public class Character {
        public string id;
        public string name;
        public string[] lines;
    }
    [System.Serializable]
    public class CharacterData {
        public Character[] characters;
    }

    Dictionary<string, string> names = new Dictionary<string, string>();
    Dictionary<string, string[]> lines = new Dictionary<string, string[]>();

    bool initialized = false;
    void Init() {
        if (initialized) {
            return;
        }
        CharacterData characterData = JsonUtility.FromJson<CharacterData>(linesJson.text);
        foreach (Character character in characterData.characters) {
            names[character.id] = character.name;
            lines[character.id] = character.lines;
        }
        initialized = true;
    }

    void Start() {
        Init();
    }

    public string GetName(string id) {
        Init();
        return names[id];
    }

    public List<string> GetLines(string id) {
        Init();
        return lines[id].ToList();
    }
}
