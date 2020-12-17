using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    void Start() {
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        explosion.Play();
        Destroy(gameObject, explosion.main.duration);
    }
}
