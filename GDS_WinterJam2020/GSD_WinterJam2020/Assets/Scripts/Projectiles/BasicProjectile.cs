using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile {
    public float speed;

    [System.NonSerialized]
    public Vector3 vel;

    [System.NonSerialized]
    public float damage;

    public float bulletLifetime = 1.5f;

    public Team team;

    public override void initialize(float speed, Vector3 dir, float damage, GameObject target) {
        this.speed = speed;
        this.vel = dir.normalized * speed;
        this.damage = damage;
    }

    public void Update() {
        if (bulletLifetime < 0.0f)
            Destroy(gameObject);
    }

    public void FixedUpdate() {
        transform.position += vel * Time.deltaTime;
        bulletLifetime -= Time.deltaTime;
    }

    public override Team getTeam() => team;

    public override void setTeam(Team t) => team = t;

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject otherObj = collision.gameObject;

        if (team == Team.Enemy && otherObj.tag == "Enemy") {
            return;
        }

        Debug.Log("aaa");
        switch (team) {
            case Team.Player:
                if (otherObj.tag == "Enemy") {
                    EnemyBase enemy = otherObj.GetComponent<EnemyBase>();
                    enemy.hp -= (int) damage;
                }
                break;
            case Team.Enemy:
                if (otherObj.tag == "Player") {
                    Player player = otherObj.GetComponent<Player>();
                    if (player != null) {
                        player.damage();
                    }
                }
                break;
        }
        Destroy(gameObject);
    }
}
