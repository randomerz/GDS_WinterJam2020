using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public GameObject keyOn;
    public GameObject keyOff;

    public Player player;

    private void Update()
    {
        SetHealth(player.HP);
    }

    public void SetHealth(int hp)
    {
        healthText.text = hp.ToString();
    }

    public void SetKeyState(bool isOn)
    {
        keyOn.SetActive(isOn);
        keyOff.SetActive(!isOn);
    }
}
