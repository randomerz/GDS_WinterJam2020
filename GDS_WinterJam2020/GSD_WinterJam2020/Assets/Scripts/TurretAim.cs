using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : AimBehavior
{
    private List<GameObject> targets = new List<GameObject>();
    public float turnSpeed = 50.0f;

    public TurretAim(float turnSpeed) {
        this.turnSpeed = turnSpeed;
    }

    private void refreshTargetsList()
    {
        targets.Clear();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public override GameObject getTarget() {
        refreshTargetsList();

        float minDist = Mathf.NegativeInfinity;
        GameObject closestEnemy = targets[0];

        foreach (GameObject e in targets) {
            float dist = (e.transform.position - transform.position).magnitude;
            if (dist < minDist) {
                minDist = dist;
                closestEnemy = e;
            }
        }

        return closestEnemy;
    }

    public override float getAngle(Transform transform)
    {
        GameObject target = getTarget();

        float angle = 
            Mathf.Atan2(
                    target.transform.position.y - transform.position.y, 
                    target.transform.position.x -transform.position.x ) 
            * Mathf.Rad2Deg;

        return angle - 90.0f;
    }
}
