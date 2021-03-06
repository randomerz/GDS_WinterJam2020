﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Standing,
    Running,
    Dashing,
    Parrying,
}

public class Ability {
    public Timer cooldownTimer;
    public Timer durationTimer;
    

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

public class Player : MonoBehaviour
{
    
    private bool dead = false;

    public int ammo = 0;
    public int maxAmmo = 4;
    public bool canPlaceTurrets = true;
    public float moveSpeed = 5.0f;

    public int HP = 5;

    public Animator playerAnim;
    public SpriteRenderer playerSprite;
    
    private State state;

    public float turretSpeed;
    public GameObject turretPrefab;

    public float dashTime = 0.25f;
    public float dashCooldown = 0.5f;
    private Ability dashAbility;

    public float parryTime = 0.25f;
    public float parryCooldown = 5.0f;
    private bool parrySoundPlayed = true;

    public GameObject shield;
    private Ability parryAbility;

    public float dashDamping = 0.9f;
    public float runDamping = 0.5f;

    public float dashMultiplier = 5.0f;

    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    public float flashTime = 0.25f;
    private Timer flashTimer;

    public SpriteRenderer sprite;
    public ParticleSystem dashParticles;
    public GameObject turretDeathParticle;
    public GameObject wrenchObj;

    public bool canDie = false;
    public GameObject gameOverUIPanel;

    public float startTime;

    private AudioManager.AudioManager audioManager;
    private bool runningSound = false;

    static Player instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
            startTime = Time.time;
        }
        else if (instance != this)
        {
            instance.gameObject.transform.position = transform.position;
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
        }
    }

    void Start()
    {
        dashAbility = new Ability(dashCooldown, dashTime);
        parryAbility = new Ability(parryCooldown, parryTime);
        flashTimer = new Timer(flashTime);
        //sprite = GameObject.Find("tempPlayerSprite").GetComponent<SpriteRenderer>();
        audioManager = AudioManager.AudioManager.m_instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Wall")
        {
            state = State.Running;
        }
    }

    void Update()
    {
        if (dead)
        {
            return;
        }
        //Debug.Log("state: " + state);

        // velocity control
        switch (state) {
            case State.Dashing: velocity *= dashDamping; break;
            default: velocity *= runDamping; break;
        }
        if (state == State.Running)
        {
            if (!runningSound)
            {
                audioManager.PlaySFX(0);
                runningSound = true;
            }            
        }
        else
        {
            if (runningSound)
            {
                audioManager.PauseSFX(0);
                runningSound = false;
            }
            
        }

        if (state != State.Dashing) {
            velocity.x += Input.GetAxisRaw("Horizontal") * moveSpeed;
            velocity.y += Input.GetAxisRaw("Vertical") * moveSpeed;
        }

        if (Input.GetButtonDown("Fire2")) {
            placeTurret();
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
                if (dashAbility.isFinished())
                {
                    setState(State.Running);
                    goto case State.Running; // ur always 0 after dash so it goes to standing and then running anyway :(
                }
                break;
            case State.Parrying:
                if (parryAbility.isFinished())
                {
                    setState(State.Running);
                    goto case State.Running;
                }
                break;
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
        wrenchObj.transform.rotation = Quaternion.Euler(0, 0, fAngle);
    }

    void setState(State newState) {
        //Debug.Log("switching from " + state + " to " + newState);

        if (newState == State.Dashing)
        {
            dashParticles.Play();
            float rot = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            dashParticles.gameObject.transform.rotation = Quaternion.Euler(0, 0, rot);
        }
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
            audioManager.PlayOneShotSFX(8);
            setState(State.Dashing);
        }
    }

    void handleParry() {
        if (state != State.Dashing && parryAbility.isAvailable()) {
            parrySoundPlayed = false;
            parryAbility.reset();

            GameObject shieldInstance = 
                Instantiate(shield, new Vector3(transform.position.x, transform.position.y, 1.0f), Quaternion.identity);

            shieldInstance.name = "shield";

            shieldInstance.transform.parent = transform;
            audioManager.PlayOneShotSFX(5);
            setState(State.Parrying);
        }
    }

    void updateColor() {
        if (flashTimer.isRunning()) {
            float saturation = 0.75f;
            float red = flashTimer.progress() * saturation;
            
            // to go from red to normal gotta go from (255, 0, 0) to (255, 255, 255)
            sprite.color = new Color(1.0f, 1.0f - red, 1.0f - red, 1.0f);
        }
    }

    public void damage()
    {
        if (state != State.Parrying && flashTimer.isFinished()) {
            HP -= 1;
            flashTimer.reset();
            audioManager.PlaySFX(1);

            if (canDie && HP <= 0)
            {
                GameObject.Find("CanvasUI").GetComponent<ActivateGameOverPanel>().ActivatePanel();
                dead = true;
            }
        }
    }

    public void restartPlayer()
    {
        HP = 7;
        ammo = 4;
        dead = false;
    }

    public void gainHP(int hp)
    {
        HP += 1;
        audioManager.PlayOneShotSFX(4);
    }

    private void FixedUpdate()
    {
        dashAbility.update(Time.deltaTime);
        parryAbility.update(Time.deltaTime);
        if (parryAbility.isAvailable() && !parrySoundPlayed)
        {
            parrySoundPlayed = true;
            audioManager.PlayOneShotSFX(6);
        }

        flashTimer.update(Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
        updateColor();
    }

    public void addAmmo() {
        ammo = Mathf.Min(maxAmmo, ammo + 1);
    }

    public void resetShield() {
        parryAbility.reset();
    }

    public void reduceShieldCD() {
        parryAbility.cooldownTimer.timeRemaining /= 2;
    }

    public void placeTurret() {
        if (ammo >= maxAmmo && canPlaceTurrets)
        {
            //if (ammo > 0 && canPlaceTurrets) {
            //ammo -= 1;
            ammo = 0;
            float rot = wrenchObj.transform.eulerAngles.z * Mathf.Deg2Rad;
            Debug.Log("Before: " + wrenchObj.transform.rotation.z + " After: " + rot);
            Vector3 turretVel = new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0.0f) * turretSpeed;

            //Debug.Log(turretPrefab);

            GameObject turret = Instantiate(turretPrefab, transform.position, wrenchObj.transform.rotation);
            TurretSlide slide = turret.gameObject.AddComponent<TurretSlide>() as TurretSlide;
            slide.setVel(turretVel);
        }
   }

    public void destroyAllTurrets()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject g in turrets)
        {
            GameObject dp = Instantiate(turretDeathParticle, g.transform.position, g.transform.rotation);
            Destroy(g);
            Destroy(dp, 3);
        }
        StartCoroutine(refillAmmoCount());
    }

    private IEnumerator refillAmmoCount()
    {
        canPlaceTurrets = false;
        while (ammo < maxAmmo / 2)
        {
            ammo += 1;
            yield return new WaitForSeconds(.2f);
        }
        canPlaceTurrets = true;
    }
}
