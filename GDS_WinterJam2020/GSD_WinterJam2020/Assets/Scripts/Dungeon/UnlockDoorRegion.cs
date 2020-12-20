using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorRegion : MonoBehaviour
{
    public Room room;
    private bool hasCollectedKey = false;
    private AudioManager.AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollectedKey)
            room.UnlockRoom();
    }
    private void Start()
    {
        audioManager = AudioManager.AudioManager.m_instance;
    }

    public void CollectKey()
    {
        hasCollectedKey = true;
        audioManager.PlayOneShotSFX(4);
    }
}
