using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLogic : MonoBehaviour
{
    public Animator Animator;

    void Start()
    {
        Animator.SetBool("shot", false);
    }

    public void Shot()
    {
        Animator.SetBool("shot", true);
        Animator.SetBool("shot", false);
    }
}
