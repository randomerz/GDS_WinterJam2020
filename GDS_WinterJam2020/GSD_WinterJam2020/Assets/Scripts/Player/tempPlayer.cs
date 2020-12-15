using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Standing,
    Running,
    Dashing,
    Parrying,
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

public class Ability {
    private Timer cooldownTimer;
    private Timer durationTimer;

    public Ability(float cooldown, float duration) {
        cooldownTimer = new Timer(cooldown);
        durationTimer = new Timer(duration);
    }

    public void update(float deltaTime) {
        cooldownTimer.update(deltaTime);
        durationTimer.update(deltaTime);
    }

    public bool isAvailable() {
        return cooldownTimer.isFinished() && durationTimer.isFinished();
    }

    public bool isRunning() {
        return durationTimer.isRunning();
    }

    public bool isFinished() {
        return durationTimer.isFinished();
    }

    public void reset() {
        cooldownTimer.reset();
        durationTimer.reset();
    }
}

public class tempPlayer : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Animator playerAnim;
    public SpriteRenderer playerSprite;

    private State state;

    public float dashTime = 0.25f;
    public float dashCooldown = 0.5f;
    private Ability dashAbility;

    public float parryTime = 0.25f;
    public float parryCooldown = 5.0f;
    public GameObject shield;
    private Ability parryAbility;

    public float dashDamping = 0.9f;
    public float runDamping = 0.5f;

    public float dashMultiplier = 5.0f;

    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        dashAbility = new Ability(dashCooldown, dashTime);
        parryAbility = new Ability(parryCooldown, parryTime);
    }

    void Update()
    {
        //Debug.Log("state: " + state);

        // velocity control
        switch (state) {
            case State.Dashing: velocity *= dashDamping; break;
            default: velocity *= runDamping; break;
        }

        if (state != State.Dashing) {
            velocity.x += Input.GetAxisRaw("Horizontal") * moveSpeed;
            velocity.y += Input.GetAxisRaw("Vertical") * moveSpeed;
        }

        if (velocity.magnitude <= 0.05f) {
            velocity.x = 0.0f;
            velocity.y = 0.0f;
            velocity.z = 0.0f;
        }

        // dash stuff

        if (Input.GetKeyDown(KeyCode.Space)) {
            handleDash();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            handleParry();
        }

        switch (state) {
            case State.Standing:
                if (velocity.magnitude > 0.0f) setState(State.Running);
                break;
            case State.Running:
                if (velocity.magnitude == 0.0f) setState(State.Standing);
                break;
            case State.Dashing:
                if (dashAbility.isFinished()) setState(State.Standing);
                goto case State.Standing;
            case State.Parrying:
                if (parryAbility.isFinished()) setState(State.Standing);
                goto case State.Standing;
            default:
                break;
        }

        if ((state == State.Running || state == State.Parrying) 
                && velocity.magnitude > moveSpeed) {
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

    void setState(State newState) {
        Debug.Log("switching from " + state + " to " + newState);

        if (state == State.Parrying && newState != State.Parrying) {
            GameObject shieldInstance = transform.Find("shield").gameObject;
            Destroy(shieldInstance);
        }

        state = newState;
        playerAnim.SetBool("isRunning", state == State.Running);
        playerAnim.SetBool("isDashing", state == State.Dashing);
    }

    void handleDash() {
        if (state != State.Dashing && dashAbility.isAvailable()) {
            //Debug.Log("cooldown: " + cooldownTimer.timeRemaining);
            dashAbility.reset();
            velocity *= dashMultiplier;
            setState(State.Dashing);
        }
    }

    void handleParry() {
        if (state != State.Dashing && parryAbility.isAvailable()) {
            parryAbility.reset();

            GameObject shieldInstance = 
                Instantiate(shield, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);

            shieldInstance.name = "shield";

            shieldInstance.transform.parent = transform;
            setState(State.Parrying);
        }
    }

    private void FixedUpdate()
    {
        dashAbility.update(Time.deltaTime);
        parryAbility.update(Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }
}
