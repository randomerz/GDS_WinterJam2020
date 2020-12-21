using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        float startTime = GameObject.Find("Player").GetComponent<Player>().startTime;
        float minutes = Mathf.Floor((Time.time - startTime) / 60);
        float seconds = Mathf.RoundToInt((Time.time - startTime) % 60);
        timeText.text = "Time: " + minutes + ";" + seconds;
    }
}
