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
        timeText.text = "Time: " + (Time.time - startTime);
    }
}
