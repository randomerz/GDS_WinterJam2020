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

        Vector3 targetDir = (target.transform.position - transform.position).normalized;

        Quaternion dir = new Quaternion();
        dir.SetLookRotation(targetDir, Vector3.up);

        float current_angle = transform.rotation.eulerAngles.z;
        //Debug.Log("Current: " + current_angle);
        float target_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        //Debug.Log("Target: " + target_angle);
        float diff = target_angle - current_angle;
        //Debug.Log("diff:" + diff);
        if (diff > 180 || diff < -180)
        {
            diff = -diff;
        }
        float incr = turnSpeed * (diff > 0 ? 1 : -1) * Time.deltaTime;

        return current_angle + (Mathf.Abs(incr) > Mathf.Abs(diff) ? diff : incr);
    }
}

