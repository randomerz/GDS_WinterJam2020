using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlide : MonoBehaviour {
    private Vector3 vel;
    public float damping = 0.9f;

    void Start() { }

    void Update() {
        Debug.Log("Vel: " + vel);
        vel *= damping;

        if (vel.magnitude < 0.05f) {
            vel.x = 0.0f;
            vel.y = 0.0f;
            vel.z = 0.0f;
            Destroy(this);
        }
    }

    void FixedUpdate() {
        transform.position += vel * Time.deltaTime;
    }

    public void setVel(Vector3 v) {
        vel = v;
    }
}
