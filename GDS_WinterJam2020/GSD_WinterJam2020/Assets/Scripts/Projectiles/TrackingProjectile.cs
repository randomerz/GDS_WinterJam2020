using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : Projectile {
    public float speed;
    public float trackingAccel;
    private Vector3 vel;
    private float damage;

    private float lifetime = 0;

    private GameObject target;

    public override void initialize(float speed, Vector3 dir, float damage, GameObject target) {
        this.speed = speed;
        this.vel = dir.normalized * speed;
        this.damage = damage;
        this.target = target;
        this.target = target;
    }

    public void Update() {
        if (lifetime > 1.5)
            Destroy(gameObject);

        Vector3 targetDir = (target.transform.position - transform.position).normalized;
        vel += targetDir * trackingAccel;
        vel = vel.normalized;
        vel *= speed;
    }

    public void FixedUpdate() {
        transform.position += vel * Time.deltaTime;
        lifetime += Time.deltaTime;
    }
    
    public override float getDamage() => damage;
}
