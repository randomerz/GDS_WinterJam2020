using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject otherObj = collision.gameObject;
        Debug.Log("Shield hit, tag: " + otherObj.tag);

        if (otherObj.tag == "Bullet" ) {
            TurretBase tmp;
            if ((tmp = otherObj.GetComponent<TurretBase>()) != null && tmp.team == Team.Enemy)
            {
                GameObject playerObj = transform.parent.gameObject;
                Player player = playerObj.GetComponent<Player>();

                player.addAmmo();
                player.resetShield();

                Destroy(otherObj);
            }
        }
    }
}
