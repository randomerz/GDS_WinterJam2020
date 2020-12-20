using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAim : AimBehavior
{
    private List<GameObject> targets = new List<GameObject>();
    public float turnSpeed = 50.0f;

    public DefaultAim(float turnSpeed) {
        this.turnSpeed = turnSpeed;
    }

    private void refreshTargetsList()
    {
        // get all the enemies in the scene
        // rn im just making it the player

        targets.Clear();
        targets.Add(GameObject.Find("Player"));
    }

    public override GameObject getTarget() {
        refreshTargetsList();
        return targets[0];
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
