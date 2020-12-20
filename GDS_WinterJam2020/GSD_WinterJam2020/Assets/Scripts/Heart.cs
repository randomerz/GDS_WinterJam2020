using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    void Start() {}

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().gainHP(1);
        }
    }
}
