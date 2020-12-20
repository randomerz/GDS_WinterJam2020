using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnimation : MonoBehaviour
{
    public bool randomStart;
    public float o;
    private Vector3 r;

    void Start()
    {
        if (randomStart)
            transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        r = new Vector3(0, 0, o);
    }

    void Update()
    {
        transform.Rotate(r * Time.deltaTime);
    }
}
