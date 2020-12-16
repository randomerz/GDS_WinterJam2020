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

    public Projectile projectile;

    private Timer fireTimer;

    private AimBehavior aimAlgo;

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = new Timer(fireRate);
        aimAlgo = new DefaultAim(turnSpeed);
    }

    void shoot() {
        // idk how this works
        fireTimer.reset();
        Projectile p = 
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);

        float angle = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
        p.initialize(projectileSpeed, dir, projectileDamage, aimAlgo.getTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished()) shoot();

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAlgo.getAngle(transform));
    }

    void FixedUpdate() {
        fireTimer.update(Time.deltaTime);
    }
}
