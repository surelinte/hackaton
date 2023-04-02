using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public enum State { Idle, Start, Roll, Slow, Stop };
    public State state;

    public float speed = 1;
    public float timeMin = 2;
    public float timeMax = 3;
    public float startTime = 0.5f;
    public float slowTime = 0.5f;
    public float stopTime = 1;

    float slowSpeed;
    float rotateSpeed;
    float timer;

    event System.Action onStopped;
    AudioSource source;

    void Start() {
        slowSpeed = speed / slowTime;
        source = FindObjectOfType<AudioSource>();
    }

    public void Subscribe(System.Action action) {
        onStopped += action;
    }

    public void Roll() {
        if (state == State.Idle) {
            Sound.Play("drumRoll");
            rotateSpeed = 0;
            timer = startTime;
            state = State.Start;
        }
    }

    void Update() {
        if (state == State.Idle) {
            return;
        }
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (state == State.Start) {
            rotateSpeed += slowSpeed * Time.deltaTime;
            if (rotateSpeed > speed) {
                rotateSpeed = speed;
            }
            if (timer < 0) {
                timer = Random.Range(timeMin, timeMax);
                state = State.Roll;
            }
        }
        else if (state == State.Roll) {
            if (timer < 0) {
                timer = slowTime;
                state = State.Slow;
            }
        }
        else if (state == State.Slow) {
            source.volume = timer / slowTime;
            rotateSpeed -= slowSpeed * Time.deltaTime;
            if (rotateSpeed < 0) {
                rotateSpeed = 0;
            }
            if (timer < 0) {
                timer = stopTime;
                state = State.Stop;
            }
        }
        if (state == State.Stop) {
            source.Stop();
            source.volume = 1;
            if (timer < 0) {
                timer = 0;
                state = State.Idle;
                onStopped?.Invoke();
            }
        }
    }
}
