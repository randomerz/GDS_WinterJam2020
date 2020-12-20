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

        //Vector3 dir = (target.transform.position - transform.position).normalized;

        //return Vector3.RotateTowards(transform.up, );

        /* float current_angle = transform.rotation.eulerAngles.z; */
        /* Debug.Log("Current: " + current_angle); */
        /* float target_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f; */
        //Debug.Log("X: " + dir.x + " Y: " + dir.y);
        /* Debug.Log("Target: " + target_angle); */
        /* float diff = target_angle - current_angle; */
        /* if (diff > 180 || diff < -180) */
        /* { */
        /*     diff = -diff; */
        /* } */

        /* diff %= 360; */
        /* Debug.Log("diff:" + diff); */

        /* float incr = turnSpeed * (diff > 0 ? 1.0f : -1.0f) * Time.deltaTime; */

        /* if (Mathf.Abs(incr) < Mathf.Abs(diff)) { */
        /*     return current_angle + incr; */
        /* } else { */
        /*     return current_angle + diff; */
        /* } */
    }
}
