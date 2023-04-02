using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLogic : MonoBehaviour
{
 public Animator anim;

    void Start()
    {
        anim.ResetTrigger("shot");
    }

    public void Shot()
    {
       // Animator anim = this.GetComponent<Animator>();
        //  anim.Play("Trigger", 0, 1);
        // anim.Play("Trigger_on");
        anim.SetTrigger("shot");
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("Audio/click");
        player.PlayOneShot(click);
    }

    public void Reset()
    {
        //Animator.SetBool("shot", false);
    }
}
