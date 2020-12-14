using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Standing,
    Running,
    Dashing,
}

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
}

public class tempPlayer : MonoBehaviour
{
    public float moveSpeed;

    public Animator playerAnim;
    public SpriteRenderer playerSprite;

    private State state;

    public float dashTime;
    public float dashCooldown;
    private Timer dashTimer;
    private Timer dashCooldownTimer;

    public float dashDamping = 0.95f;
    public float runDamping = 0.5f;

    public float dashMultiplier = 10.0f;

    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        dashTimer = new Timer(dashTime);
        dashCooldownTimer = new Timer(dashCooldown);
    }

    void Update()
    {
        //Debug.Log(dashTimer.timeRemaining);

        switch (state) {
            case State.Running: velocity *= runDamping; break;
            case State.Dashing: velocity *= dashDamping; break;
            default: break;
        }

        /* velocity /= state switch { */
        /*     State.Running => runDamping, */
        /*     State.Dashing => dashDamping, */
        /*     State.Standing => 1, */
        /* }; */

        if (state != State.Dashing) {
            velocity.x += Input.GetAxisRaw("Horizontal") * moveSpeed;
            velocity.y += Input.GetAxisRaw("Vertical") * moveSpeed;
        }

        if (velocity.magnitude <= 0.05f) {
            velocity.x = 0.0f;
            velocity.y = 0.0f;
            velocity.z = 0.0f;
        }

        if (Input.GetKeyDown("space")) {
            handleDash();
        }

        if (state == State.Dashing) {
            dashTimer.update(Time.deltaTime);
            if (dashTimer.isFinished()) {
                setState(State.Standing);
            }
        }

        if (state != State.Dashing && velocity.magnitude > 0) {
            setState(State.Running);
        } else if (state == State.Running && velocity.magnitude == 0) {
            setState(State.Standing);
        }

        if (state == State.Running && velocity.magnitude > moveSpeed) {
            velocity = velocity.normalized * moveSpeed;
        }

        // mouse
        // Project the mouse point into world space at
        //   at the distance of the player.
        Vector3 v3Pos = Input.mousePosition;
        v3Pos.z = (transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - transform.position;
        float fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f) fAngle += 360.0f;
        //Debug.Log("1) " + fAngle + " " + (90 < fAngle && fAngle < 270));

        playerSprite.flipX = 90 < fAngle && fAngle < 270;
    }

    void setState(State s) {
        state = s;
        playerAnim.SetBool("isRunning", state == State.Running);
        //playerAnim.SetBool("isDashing", state == State.Dashing);
    }

    void handleDash() {
        Debug.Log(dashCooldownTimer.timeRemaining);
        if (state != State.Dashing && dashCooldownTimer.isFinished()) {
            dashCooldownTimer.reset();
            dashTimer.reset();
            velocity *= dashMultiplier;
            setState(State.Dashing);
        }
    }

    private void FixedUpdate()
    {
        dashTimer.update(Time.deltaTime);
        dashCooldownTimer.update(Time.deltaTime);
        transform.position += velocity;
    }
}
