using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour {
    public Vector3 vel;

    void Start() {
        vel = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void FixedUpdate() {
        transform.position += vel * Time.deltaTime;
    }
}
