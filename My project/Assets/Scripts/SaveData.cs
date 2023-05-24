using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    [SerializeField] TMP_InputField textBox;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] private Toggle check;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Check"))
        {
            check.isOn = PlayerPrefs.GetInt("Check") == 1;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("Name", textBox.text);
        PlayerPrefs.SetInt("Check",check.isOn ? 1 : 0);
        infoText.text = "Your Data Saved";
    }
    public void Next()
    {
        SceneManager.LoadScene(1);
    }
    public void Default()
    {
        PlayerPrefs.DeleteAll();
    }
}
