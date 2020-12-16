using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile {
    public float speed;
    private Vector3 vel;
    private float damage;

    private float lifetime = 0;

    public override void initialize(float speed, Vector3 dir, float damage) {
        this.speed = speed;
        this.vel = dir.normalized * speed;
        this.damage = damage;
    }

    public void Update() {
        if (lifetime > 1.0)
            Destroy(gameObject);
    }

    public void FixedUpdate() {
        transform.position += vel * Time.deltaTime;

        lifetime += Time.deltaTime;
    }
    
    public override float getDamage() => damage;
}
