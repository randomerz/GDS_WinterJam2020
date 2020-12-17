using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorRegion : MonoBehaviour
{
    public Room room;
    private bool hasCollectedKey = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollectedKey)
            room.UnlockRoom();
    }

    public void CollectKey()
    {
        hasCollectedKey = true;
    }
}
