using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Fries : MonoBehaviour
{
    LevelManager levelManager;
    private int friesValue;
    private void Awake()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
    private void Start()
    {
        friesValue = Random.Range(1,10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Fries'in triggerina carpan(giren) seyin tagi "player" ise once fries'i yok et daha sonrasinda ise LevelManager scripti icerisindeki .FriesSpawner metodu ile yeni bir Fries spawnla(olustur) 
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            levelManager.count++;
            ScoreManager.instance.AddPoint(friesValue);
            levelManager.StartDelayFries();            
        }
    }
}
