using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjetile : BasicProjectile {
    public float trackingAccel;
    private GameObject target;

    public override void initialize(float speed, Vector3 dir, float damage, GameObject target) {
        base.initialize(speed, dir, damage, target);
    }
}
