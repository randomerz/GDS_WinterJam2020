using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Collider2D[] doorColliders;
    public Animator[] doorAnimators;
    public bool isOpen;
    
    public List<GameObject> enemies = new List<GameObject>();

    private bool isExplored = false;

    private float temp;

    void Start()
    {
        foreach (Collider2D c in doorColliders)
            c.enabled = !isOpen;
        foreach (Animator a in doorAnimators)
            a.SetBool("isOpen", isOpen);
        foreach (GameObject g in enemies)
            g.SetActive(!isOpen);
    }


    void Update()
    {
        if (!isOpen)
            temp += Time.deltaTime;

        int activeEnemyCount = 0;
        foreach (GameObject g in enemies)
            if (g.activeSelf)
                activeEnemyCount++;

        //if (activeEnemyCount == 0)
        if (temp > 5)
        {
            temp = 0;
            ChangeDoorState(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExplored)
            ChangeDoorState(false);
        isExplored = true;
    }

    private void ChangeDoorState(bool s)
    {
        isOpen = s;
        foreach (Collider2D c in doorColliders)
            c.enabled = !isOpen;
        foreach (Animator a in doorAnimators)
            a.SetBool("isOpen", isOpen);
        foreach (GameObject g in enemies)
            g.SetActive(!isOpen);
    }
}
