using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    private Player player;
    public int amountHeal = 1;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void HealPickedUp()
    {
        player.gainHP(amountHeal);
    }
}
