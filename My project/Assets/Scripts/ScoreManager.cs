using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    public static int score = 0;
    public static int highScore;
    private void Start()
    {
        scoreText.text = "Score : " + score.ToString();
    }
    public void AddPoint(int value)
    {
        score += value;
        scoreText.text = "Score : " + score.ToString();

        if(highScore < score)
        {
            PlayerPrefs.SetInt("High Score", score);
        }
    }
}