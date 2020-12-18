using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public bool isOptionsOpened = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOptionsOpened)
            {
                CloseOptions();
            }
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}