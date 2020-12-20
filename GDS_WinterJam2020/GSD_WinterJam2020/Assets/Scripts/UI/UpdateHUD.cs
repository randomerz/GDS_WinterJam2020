using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public GameObject keyOn;
    public GameObject keyOff;

    public Player player;

    private void Update()
    {
        SetHealth(player.HP);
        SetAmmo(player.ammo);
    }

    public void SetHealth(int hp)
    {
        healthText.text = hp.ToString();
    }

    public void SetAmmo(int a)
    {
        ammoText.text = a.ToString();
    }

    public void SetKeyState(bool isOn)
    {
        keyOn.SetActive(isOn);
        keyOff.SetActive(!isOn);
    }
}
