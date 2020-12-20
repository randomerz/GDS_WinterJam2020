using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] doorsOpen;
    public GameObject[] doorsClose;
    public ParticleSystem[] doorParticles;

    public List<GameObject> enemies1 = new List<GameObject>();
    public List<GameObject> enemies2 = new List<GameObject>();
    private bool spawnedWave1 = false;
    private bool spawnedWave2 = false;
    private bool finishedWaves = false;

    public bool isOpen = true;
    public bool isLocked = false;
    public bool isExplored = false;

    void Awake()
    {
        ChangeDoorState(isOpen);
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

            if (spawnedWave1 && !spawnedWave2 && activeEnemy1Count == 0)
            {
                StartCoroutine(SpawnWave2());
            }

            int activeEnemy2Count = 0;
            foreach (GameObject g in enemies2)
                if (g.activeSelf)
                    activeEnemy2Count++;

            if (spawnedWave2 && !finishedWaves && activeEnemy2Count == 0)
            {
                //ChangeDoorState(true);
                StartCoroutine(ShakeDoorThenOpen());
                finishedWaves = true;
                GameObject.Find("Player").GetComponent<Player>().destroyAllTurrets();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExplored)
        {
            ChangeDoorState(false);
            StartCoroutine(SpawnWave1());
        }
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
        foreach (GameObject g in doorsOpen)
            g.SetActive(s);
        foreach (GameObject g in doorsClose)
            g.SetActive(!s);
        foreach (ParticleSystem p in doorParticles)
            p.Play();
    }

    private IEnumerator ShakeDoorThenOpen()
    {
        float dt = 0.1f;
        float totalTime = 1;
        Vector3[] origPos = new Vector3[doorsClose.Length];
        for (int i = 0; i < doorsClose.Length; i++)
            origPos[i] = doorsClose[i].transform.position;

        for (float i = 0; i < totalTime; i += dt)
        {
            Vector3 r = new Vector3(Random.Range(-.02f, .02f), Random.Range(-.02f, .02f));

            for (int j = 0; j < doorsClose.Length; j++)
                doorsClose[j].transform.position = origPos[j] + r;

            yield return new WaitForSeconds(dt);
        }

        for (int j = 0; j < doorsClose.Length; j++)
            doorsClose[j].transform.position = origPos[j];

        ChangeDoorState(true);
    }

    private IEnumerator SpawnWave1()
    {
        yield return new WaitForSeconds(.5f);
        foreach (GameObject g in enemies1)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
        spawnedWave1 = true;
    }

    private IEnumerator SpawnWave2()
    {
        yield return new WaitForSeconds(.5f);
        foreach (GameObject g in enemies2)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
        spawnedWave2 = true;
    }
}