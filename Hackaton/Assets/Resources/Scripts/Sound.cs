using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip click;
    public AudioClip drumRoll;
    public AudioClip enemy;
    public AudioClip fail;
    public AudioClip money;
    public AudioClip none;
    public AudioClip win;

    static AudioSource source;
    static Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        sound["click"] = click;
        sound["drumRoll"] = drumRoll;
        sound["enemy"] = enemy;
        sound["fail"] = fail;
        sound["money"] = money;
        sound["none"] = none;
        sound["win"] = win;
    }

    public static void Play(string id) {
        source.PlayOneShot(sound[id]);
    }
}
