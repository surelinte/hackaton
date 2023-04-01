using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject wheel;
    public GameObject trigger;
    
    public void Start() {
        wheel.GetComponent<Wheel>().Subscribe(() => {
            wheel.SetActive(false);
            trigger.SetActive(true);
        });
    }

    public void Roll() {

    }

    public void Shoot() {

    }

}
