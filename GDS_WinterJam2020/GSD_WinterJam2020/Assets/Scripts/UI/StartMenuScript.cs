using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    private bool isCreditsOpened = false;
    private bool isOptionsOpened = false;

    public GameObject creditsMenu;
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOptionsOpened)
            {
                CloseOptions();
            }
            if (isCreditsOpened)
            {
                CloseCredits();
            }
        }
    }

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        isCreditsOpened = true;
    }

    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
        isCreditsOpened = false;
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        isOptionsOpened = true;
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        isOptionsOpened = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}