using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBehavior : MonoBehaviour {
    public abstract float getAngle(Transform transform);
}

public class DefaultAiming : AimBehavior {
    private List<GameObject> targets = new List<GameObject>();
    public float turnSpeed;

    public DefaultAiming (float s) {
        turnSpeed = s;
    }

    private void refreshTargetsList() {
        // get all the enemies in the scene
        // rn im just making it the player

        targets.Clear();
        targets.Add(GameObject.Find("Player"));
    }

    public override float getAngle(Transform transform) {
        refreshTargetsList();
        GameObject target = targets[0];

        Vector3 targetDir = (target.transform.position - transform.position).normalized;

        Quaternion dir = new Quaternion();
        dir.SetLookRotation(targetDir, Vector3.up);

        float current_angle = transform.rotation.eulerAngles.z;
        float target_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;

        float diff = target_angle - current_angle;
        float incr = turnSpeed * (diff > 0 ? 1 : -1) * Time.deltaTime;

        return current_angle + (Mathf.Abs(incr) > Mathf.Abs(diff) ? diff : incr);
    }
}

public class TurretBase : MonoBehaviour
{
    public float turnSpeed = 50f;
    public float fireRate = 1.0f;
    public Projectile projectile;

    private Timer fireTimer;

    private AimBehavior aimAlgo;

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = new Timer(fireRate);
        aimAlgo = new DefaultAiming(turnSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAlgo.getAngle(transform));
    }
}
