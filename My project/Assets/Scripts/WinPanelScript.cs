using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private void Update()
    {
        scoreText.text = "Your Score : " + ScoreManager.score.ToString();
    }
    public void NextLevel()
    {
        int scorePoint = ScoreManager.score;
        CountManager.instance.countForWin ++;
        CountManager.instance.level++;
        scoreText.text = scorePoint.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.canMove = true;
        LevelManager.knifeStop = false;
        Movement.canDash = true;
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
