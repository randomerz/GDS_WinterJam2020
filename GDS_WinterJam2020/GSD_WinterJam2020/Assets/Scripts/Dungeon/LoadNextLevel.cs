using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public string levelName = "";

    public void LoadLevel()
    {
        GameObject.Find("Player").GetComponent<Player>().restartPlayer();
        SceneManager.LoadScene(levelName);
    }
}
