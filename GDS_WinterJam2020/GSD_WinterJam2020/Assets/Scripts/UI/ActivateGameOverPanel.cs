using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameOverPanel : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePanel()
    {
        panel.SetActive(true);
    }
}
