using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart()
    {
        GameObject.Find("Player").GetComponent<Player>().restartPlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
