using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAnimation : MonoBehaviour
{
    public bool randomStart;
    public float A;
    public float c;
    private float t;
    private Vector3 orig;

    void Start()
    {
        orig = transform.position;
        if (randomStart)
            t += Random.Range(0, 2 * Mathf.PI / c);
    }

    void Update()
    {
        t += Time.deltaTime;
        transform.position = orig + new Vector3(0, A * Mathf.Sin(c * t));
    }
}
