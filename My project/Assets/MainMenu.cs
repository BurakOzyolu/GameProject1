
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    private void Update()
    {
        ScoreManager.highScore = PlayerPrefs.GetInt("High Score");
        highScoreText.text = "High Score : " + ScoreManager.highScore.ToString();
    }
    public void Game()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quited");
    }
}
