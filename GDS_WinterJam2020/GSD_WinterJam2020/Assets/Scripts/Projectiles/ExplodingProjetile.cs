using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjetile : BasicProjectile {
    public float trackingAccel;
    public float explosionRadius = 5.0f;
    private GameObject target;

    public float rotSpeed = 1000.0f;

    public GameObject explosion;

    public override void initialize(float speed, Vector3 dir, float damage, GameObject target) {
        base.initialize(speed, dir, damage, target);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (team == Team.Enemy && collision.gameObject.tag == "Enemy") {
            return;
        }

        LayerMask mask = LayerMask.GetMask("Mobs");

        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, explosionRadius, mask);

        foreach (Collider2D collider in overlaps) {
            GameObject otherObj = collider.gameObject;

            switch (team) {
                case Team.Player:
                    if (otherObj.tag != "Player") {
                    }
                    break;
                case Team.Enemy:
                    if (otherObj.tag != "Enemy") {
                        Player player = otherObj.GetComponent<Player>();
                        if (player != null) {
                            player.damage();
                        }
                    }
                    break;
            }
        }

        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.transform.rotation = Quaternion.EulerAngles(90, 0, 0);

        Destroy(gameObject);
    }

    private void FixedUpdate() {
        transform.Rotate(0.0f, 0.0f, rotSpeed * Time.deltaTime, Space.Self);
        base.FixedUpdate();
    }
}
