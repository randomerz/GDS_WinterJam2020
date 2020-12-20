using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBehavior : MonoBehaviour {
    public abstract float getAngle(Transform transform);
    public abstract GameObject getTarget();
}

public class TurretBase : MonoBehaviour
{
    public float turnSpeed = 50f;
    public float fireRate = 1.0f;
    public float projectileSpeed = 20.0f;
    public float projectileDamage = 20.0f;

    public Team team;

    public Projectile projectile;

    private Timer fireTimer;

    public AimBehavior aimAlgo; 
    public AudioManager.AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.AudioManager.m_instance;
        fireTimer = new Timer(fireRate);
        float randDelay = Random.Range(0.0f, fireRate);
        fireTimer.addTime(randDelay);
    }

    void shoot() {
        // idk how this works
        fireTimer.reset();
        Projectile p = 
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);
        p.setTeam(Team.Enemy);

        float angle = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
        p.initialize(projectileSpeed, dir, projectileDamage, aimAlgo.getTarget());


        switch (team)
        {
            case Team.Player:
                audioManager.PlayOneShotSFX(2);
                break;
            case Team.Enemy:
                audioManager.PlayOneShotSFX(3);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished()) shoot();

        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAlgo.getAngle(transform));
        float angle = aimAlgo.getAngle(transform);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void FixedUpdate() {
        fireTimer.update(Time.deltaTime);
    }
}
