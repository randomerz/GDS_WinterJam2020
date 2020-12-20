using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctaTurretBase : TurretBase
{
    void shoot() {
        // idk how this works
        fireTimer.reset();
        //GameObject target = aimAlgo.getTarget();
        //float angle = ((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
        for (int i = 0; i < 8; i++)
        {
            Projectile p =
                Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);
            p.setTeam(team);

            float tmp_angle = i*(Mathf.PI/4);
            Vector3 dir = new Vector3(Mathf.Cos(tmp_angle), Mathf.Sin(tmp_angle), 0.0f);
            p.initialize(projectileSpeed, dir, projectileDamage, null);
        }

        audioManager.PlayOneShotSFX(3);
              
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished()) shoot();

        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAlgo.getAngle(transform));
        float angle = aimAlgo.getAngle(transform);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void FixedUpdate() {
        fireTimer.update(Time.deltaTime);
    }
}
