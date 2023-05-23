using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnPos;
    [SerializeField] GameObject friesPrefab;
    [SerializeField] public int count;
    [SerializeField] GameObject door;
    [SerializeField] GameObject runText;

    [Header("HeaderSpawner")]
    [SerializeField] GameObject knifePrefab;
    [SerializeField] Vector2 spawnValues;
    [SerializeField] float startSpawn;
    [SerializeField] float minSpawn;
    [SerializeField] float maxSpawn;
    [SerializeField] float startWait;
    public static bool knifeStop;
    private float xPos = 10f;

    [Header("Bool")]
    public bool canWin;
    public static bool canMove = true;

    [Header("Mode Spawn")]
    [SerializeField] float easySpawn;
    [SerializeField] float normalSpawn;
    [SerializeField] float hardSpawn;


    // Awake : Starttan once calisir. Genelde sahne baslatma ve referans alma islemleri icin kullanilir. 
    private void Awake()
    {
        PlayerSpawner(); // Playerimizi olusturduk.
    }
    private void Start()
    {
        StartDelayFries();
        StartCoroutine(CreateKnife());
        maxSpawn = HardenedScript.instance.HardenedLevel(maxSpawn, easySpawn, normalSpawn, hardSpawn);
        canMove = true;
    }
    private void Update()
    {
        startSpawn = Random.Range(minSpawn, maxSpawn);
    }


    // Instantiate : GameObject olusturmamizi saglar. Bizden 3 adet parametre ister. Bunlar sirasiyla olusturmak istedigimiz nesne, nesnenin olusturacagi pozisyon ve rotasyonu (yonu)
    // Quaternion.identity : Rotation degerlerini otomatik 0 ayarlayan kod.

    void PlayerSpawner() // Playeri spawnlayan metod.
    {
        Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity);
    }
    public void RespawnPlayer() // Playeri yeniden spawnlayan metod.
    {
        Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity);
    }
    public void FriesSpawner() // Frieslari spawnlayan metod.
    {
        // Frieslarimizi oyun icerisinde rastgele yerlerde spawnlamak icin Rastgele x ve y degeri olusturan bir Vector3 olusturuyoruz.
        Vector3 spawnPos = new Vector3(Random.Range(-8.4f,8.4f),Random.Range(-4,0),0); 
        Instantiate(friesPrefab, spawnPos, Quaternion.identity);
    }

    public void StartDelayFries()
    {
        StartCoroutine(DelayFries());
    }

    public IEnumerator DelayFries()
    {
        if(count == 5)
        {
            canWin = true;
            door.SetActive(true);
            knifeStop = true;
            runText.SetActive(true);
            SoundManager.instance.PlayWithIndex(10);
        }

        yield return new WaitForSeconds(1.5f);

        if(count < 5)
        {
            FriesSpawner();
        }
    }
    //Burada bicagi spawn ediyoruz.
    private IEnumerator CreateKnife()
    {
        yield return new WaitForSeconds(startWait);

        while(!knifeStop)
        {
            Vector2 spawnPos = new Vector2(xPos, Random.Range(-spawnValues.y, spawnValues.y));
            Instantiate(knifePrefab,spawnPos,Quaternion.identity);
            SoundManager.instance.PlayWithIndex(6);
            yield return new WaitForSeconds(startSpawn);
        }
    }
}
