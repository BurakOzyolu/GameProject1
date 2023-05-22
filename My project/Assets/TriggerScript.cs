using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPos;
    bool moveEnemy = false;

    // Dusmaninizin her bilgisayarda her kullanicida ayni sekilde hareket etmesi icin FixedUpdate icerisinde MoveEnemy() metodunu cagirdik.
    private void FixedUpdate()
    {
        MoveEnemy();
    }

    // Enemymizin olusturulmasini ve hareketini sagliyoruz. Burada moveEnemy adinda bool bir degiskenimiz var. Cunku biz bu scriptin bagli oldugu sprite uzerine geldigimiz zaman surekli olarak spawnlanmasini istemiyoruz. Trigger'a soktugumuz saniye sadece 1 tane spawnlamasi icin olusturduk.
    void MoveEnemy() 
    {
        if (moveEnemy)
        {
            SpawnEnemy();
            moveEnemy = false;
        }
    }

    // Enemy spawnlayan metod.
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPos.position, enemyPrefab.transform.rotation);
    }

    // Playerimiz scriptin bagli oldugu sprite'in triggerina girdigi saniye enemy spawnlamasi icin moveEnemy degiskenini true yapiyoruz.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveEnemy = true;
        }
    }


}
