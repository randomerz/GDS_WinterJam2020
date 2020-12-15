using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBehavior : MonoBehaviour {
    public abstract Quaternion getAngle(Transform transform);
}

public class DefaultAiming : AimBehavior {
    private List<GameObject> targets = new List<GameObject>();
    public float turnSpeed = 0.1f;

    public DefaultAiming (float s) {
        turnSpeed = s;
    }

    private void refreshTargetsList() {
        // get all the enemies in the scene
        // rn im just making it the player

        targets.Clear();
        targets.Add(GameObject.Find("Player"));
    }

    public override Quaternion getAngle(Transform transform) {
        refreshTargetsList();
        GameObject target = targets[0];

        Vector3 targetDir = (target.transform.position - transform.position);
        targetDir = targetDir.normalized;

        Vector3 currentDir = transform.forward;
        currentDir = Vector3.RotateTowards(currentDir, targetDir, turnSpeed * Time.deltaTime, 1.0f);

        //return Quaternion.Lerp(transform.rotation, rotation, turnSpeed);
        Quaternion dir = new Quaternion();
        dir.SetLookRotation(currentDir, Vector3.up);
        return dir;
    }
}

public class TurretBase : MonoBehaviour
{
    public float turnSpeed = 0.5f; // 0 to 1
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
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAlgo.getAngle(transform).z);
    }
}
