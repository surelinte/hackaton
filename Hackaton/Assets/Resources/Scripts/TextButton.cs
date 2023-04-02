using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour
{
    //private TMP_Text textToEdit;
   // private Color32 NormalColor;

    void Start()
    {
      //  textToEdit = GetComponentInChildren<TMP_Text>();
      //  NormalColor = GetComponentInChildren<TMP_Text>().color;
    }

    public void TaskOnDown()
    {
    //    Color32 DownColor = GetComponentInParent<Button>().colors.pressedColor;
    //    textToEdit.color = DownColor;
    //    transform.localScale = new Vector2(0.97f, 0.97f);
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("Audio/click");
        player.PlayOneShot(click);
    }

    public void TaskOnUp()
    {
    //    textToEdit.color = NormalColor;
   //     transform.localScale = new Vector2(1, 1);
    }
}
