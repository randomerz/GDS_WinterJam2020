using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    public float timeRemaining;
    private float startTime;

    public Timer(float time) {
        timeRemaining = 0.0f;
        startTime = time;
    }

    public void update(float deltaTime) {
        timeRemaining = Mathf.Max(timeRemaining - deltaTime, 0.0f);
    }

    public bool isFinished() {
        return timeRemaining == 0.0f;
    }

    public bool isRunning() {
        return timeRemaining > 0.0f;
    }

    public void reset() {
        timeRemaining = startTime;
    }

    public float progress() => timeRemaining / startTime;
}
