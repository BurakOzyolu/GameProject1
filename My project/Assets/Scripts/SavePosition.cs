using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SavePosition : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI playerName;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PositionX") || PlayerPrefs.HasKey("PositionY"))
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        }
        else
        {
            player.transform.position = new Vector2(0, 0);
        }
        playerName.text = PlayerPrefs.GetString("Name");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("PositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PositionY", player.transform.position.y);
        infoText.text = "Data Saved";
    }
    public void ResetPosition()
    {
        PlayerPrefs.DeleteKey("PositionX");
        PlayerPrefs.DeleteKey("PositionY");
        infoText.text = "Data Deleted";
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(2);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
