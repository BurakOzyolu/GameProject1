using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause;
    [SerializeField] GameObject pauseMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        isPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        LevelManager.canMove = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        LevelManager.canMove = true;
        ScoreManager.score = 0;
    }
    public void Pause()
    {
        isPause=true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        LevelManager.canMove = false;
    }
}
