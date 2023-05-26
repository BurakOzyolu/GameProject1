using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI levelText;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Update()
    {
        scoreText.text = "Your Score : " + ScoreManager.score.ToString();
        ScoreManager.highScore = PlayerPrefs.GetInt("High Score");
        highScoreText.text = "High Score : " + ScoreManager.highScore.ToString();
        levelText.text = "Level :" + CountManager.instance.level.ToString();
    }

    public void RestartButton() // Oyunumuzu yeniden baslatmasini saglayan metod.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // .LoadScene() : parantez icerisinde yazili olan index degerine sahip sahneyi yukler.
        canvas.enabled = false; // Oyun bittigi zaman bizim canvasimiz etkin oluyordu. Bunu devre disi birakiyoruz
        ScoreManager.score = 0;
        LevelManager.knifeStop = false;
        CountManager.instance.level = 1;
        if (PlayerPrefs.HasKey("Easy Mode")) 
        {
            CountManager.instance.countForWin = 1;
        }
        else if (PlayerPrefs.HasKey("Normal Mode"))
        {
            CountManager.instance.countForWin = 2;
        }
        else if(PlayerPrefs.HasKey("Hard Mode"))
        {
            CountManager.instance.countForWin = 3;
        }
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        ScoreManager.score = 0;
        LevelManager.knifeStop = false;
    }
}
