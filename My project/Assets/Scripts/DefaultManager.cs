using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultManager : MonoBehaviour
{
    [HideInInspector]
    public bool easyMod, normalMod, hardMod;

    void Start()
    {
        PlayerPrefs.DeleteKey("Easy Mode");
        PlayerPrefs.DeleteKey("Normal Mode");
        PlayerPrefs.DeleteKey("Hard Mode");

        easyMod = false;
        normalMod = false;
        hardMod = false;
    }

    public void EasyButton()
    {
        easyMod = true;
        PlayerPrefs.SetInt("Easy Mode", easyMod ? 1 : 0);
        SceneManager.LoadScene(1);
    }
    public void NormalButton()
    {
        normalMod = true;
        PlayerPrefs.SetInt("Normal Mode", normalMod ? 1 : 0);
        SceneManager.LoadScene(1);
    }
    public void HardButton()
    {
        hardMod = true;
        PlayerPrefs.SetInt("Hard Mode", hardMod ? 1 : 0);
        SceneManager.LoadScene(1);

    }
}
