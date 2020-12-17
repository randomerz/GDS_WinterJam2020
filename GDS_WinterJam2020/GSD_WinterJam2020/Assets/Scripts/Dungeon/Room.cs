using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Collider2D[] doorColliders;
    public Animator[] doorAnimators;

    public List<GameObject> enemies1 = new List<GameObject>();
    public List<GameObject> enemies2 = new List<GameObject>();
    private bool spawnedWave1 = false;

    public bool isOpen = true;
    public bool isLocked = false;
    public bool isExplored = false;
    
    void Start()
    {
        foreach (Collider2D c in doorColliders)
            c.enabled = !isOpen;
        foreach (Animator a in doorAnimators)
            a.SetBool("isOpen", isOpen);
        foreach (GameObject g in enemies1)
            g.SetActive(!isOpen);
        foreach (GameObject g in enemies2)
            g.SetActive(!isOpen);
    }


    void Update()
    {
        if (!isOpen && !isLocked)
        {
            int activeEnemy1Count = 0;
            foreach (GameObject g in enemies1)
                if (g.activeSelf)
                    activeEnemy1Count++;

            if (!spawnedWave1 && activeEnemy1Count == 0)
            {
                SpawnWave2();
                spawnedWave1 = true;
            }

            int activeEnemy2Count = 0;
            foreach (GameObject g in enemies2)
                if (g.activeSelf)
                    activeEnemy2Count++;

            if (spawnedWave1 && activeEnemy2Count == 0)
            {
                ChangeDoorState(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExplored)
            ChangeDoorState(false);
        isExplored = true;
    }

    public void UnlockRoom()
    {
        ChangeDoorState(true);
        isExplored = true;
    }

    private void ChangeDoorState(bool s)
    {
        isOpen = s;
        foreach (Collider2D c in doorColliders)
            c.enabled = !isOpen;
        foreach (Animator a in doorAnimators)
            a.SetBool("isOpen", isOpen);
        if (!isOpen)
            SpawnWave1();
    }

    private void SpawnWave1()
    {
        foreach (GameObject g in enemies1)
            g.SetActive(true);
    }

    private void SpawnWave2()
    {
        foreach (GameObject g in enemies2)
            g.SetActive(true);
    }
}
