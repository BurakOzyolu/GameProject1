using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    UIManager uiManager;
    [SerializeField] Image[] playerHealthIcons;
    [SerializeField] int playerLifeCount = 3;
    Delay delay;

    #region Singleton
    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Sahnede fazladan player health olamaz");
        }
    }
    #endregion

    // UIManager ve Delay scriptlerini cekiyoruz.
    private void Start()
    {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        delay = GameObject.Find("Level Manager").GetComponent<Delay>();
    }

    // Karakterimizin canini dusurmek icin olusturdugumuz metod. Calistigi zaman oncelikle cani 1 azalir ve oyun icerisindeki kalplerinden biri silinir. Daha sonra cani kaldi mi kalmadi mi onu kontrol ediyoruz. Eger cani kalmadiysa UIManagerdaki GameOver ekranini aciyoruz ve karakterin yeniden dogmasini engelliyoruz.
    public void Lives()
    {
        playerLifeCount--;
        Destroy(playerHealthIcons[playerLifeCount]);

        if (playerLifeCount < 1)
        {
            uiManager.GetComponent<Canvas>().enabled = true;
            LevelManager.knifeStop = true;
            delay.delayTime = false;
        }
    }
}
