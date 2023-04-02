using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLogic : MonoBehaviour
{
    public Animator Animator;

    void Start()
    {
      //  Animator.SetBool("shot", false);
    }

    public void Shot()
    {
     //   Animator.SetActive(true);
        Animator.Play("Trigger_on", 0, 1);
        // Animator.SetBool("shot", false);
        // Animator.SetBool("shot", true);
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("Audio/click");
        player.PlayOneShot(click);
    }

    public void Reset()
    {
        //Animator.SetBool("shot", false);
    }
}
