using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    public float run_speed = 5.0f;
    public float walk_speed = 0.5f;
    public float walk_radius = 12.5f;

    public int hp = 10;

    private Vector3 vel;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        vel = new Vector3(0.0f, 0.0f, 0.0f);
        player = GameObject.Find("Player");
        run_speed += Random.Range(-run_speed / 10.0f, run_speed / 10.0f);
    }

    // Update is called once per frame
    void Update() {
        Vector3 radius = player.transform.position - transform.position;

        vel = radius.normalized;
        vel *= (radius.magnitude > walk_radius) ? run_speed : walk_speed;

        if (hp <= 0) {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate() {
        transform.position += vel * Time.deltaTime;
    }
}
