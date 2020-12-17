using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : BasicProjectile {
    public float trackingAccel;
    private GameObject target;

    public override void initialize(float speed, Vector3 dir, float damage, GameObject target) {
        base.initialize(speed, dir, damage, target);
        this.target = target;
    }

    public new void Update() {
        base.Update();

        Vector3 targetDir = (target.transform.position - transform.position).normalized;
        vel += targetDir * trackingAccel;
        vel = vel.normalized;
        vel *= speed;
    }
}
