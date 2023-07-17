using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public GameObject InGameUI;
    public bool isPaused = false;

    public void Pause()
    {
        Time.timeScale = 0;
        InGameUI.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(sceneID);
    }
}
