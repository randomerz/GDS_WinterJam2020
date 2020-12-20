using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    private AudioManager.AudioManager audioManager;
    private void Awake()
    {
        audioManager = AudioManager.AudioManager.m_instance;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject otherObj = collision.gameObject;
        Debug.Log("Shield hit, tag: " + otherObj.tag);

        if (otherObj.tag == "Bullet" ) {
            Projectile tmp;
            if ((tmp = otherObj.GetComponent<Projectile>()) != null && tmp.getTeam() == Team.Enemy)
            {
                GameObject playerObj = transform.parent.gameObject;
                Player player = playerObj.GetComponent<Player>();

                player.addAmmo();
                player.resetShield();
                player.reduceShieldCD();
                audioManager.PlayOneShotSFX(7);

                Destroy(otherObj);

            }
        }
    }
}
