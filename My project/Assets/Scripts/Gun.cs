using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector2 direction; // Silahin bakicagi yon icin olusturdugumuz degisken
    Vector2 target; // Hedefin yon degerlerini almak icin olusturdugumuz degisken
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] GameObject gunLight;
    public bool isClose;
    [SerializeField] private float nextFire;
    [SerializeField] private float fireRate;
    [SerializeField] ParticleSystem muzzleEffect;

    [Header("Mode Rate")]
    [SerializeField] float easyRate;
    [SerializeField] float normalRate;
    [SerializeField] float hardRate;
    // Karakterimiz surekli hareket edecegi icin silahimizinda surekli olarak takip etmesi gerekiyor. Bundan dolayi GunDirection metodu Update icerisinde yazili.
    private void Start()
    {
        fireRate = HardenedScript.instance.HardenedLevel(fireRate, easyRate, normalRate, hardRate);
    }

    void Update()
    {
        GunDirection();
        GunLight();
    }

    // Silahin playeri hedeflemesi icin olusturdugumuz metod.
    void GunDirection()
    {
        // Once oyunda Player tagine sahip bir GameObjesi var mi yok mu onu kontrol ediyoruz ve var ise player degiskenine atiyoruz.
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 

        if(player != null) // Eger player var ise bizim bu kodlarimiz calisacak.
        {
            target = player.transform.position; // Once playerin pozisyonunu aliyoruz cunku silahin hedefi o olacak. 
            direction = target - (Vector2)transform.position; // Daha sonra burada hedef ile silahimizin arasindaki farki alarak bir nevi merminin gidecegi yon vektorunu aliyoruz.
            transform.right = -direction; // Nesnenin yonunu ayarladik. Silahin ters tarafindan cikmamasi icin ise - olarak aliyoruz.
        }
    }

    void GunLight()
    {
        if (isClose)
        {
            gunLight.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gunLight.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void BulletSpawn()
    {
        Instantiate(bulletPrefab, bulletSpawnPos.position, transform.rotation);
        Instantiate(muzzleEffect, bulletSpawnPos.position, Quaternion.identity);
    }

    public void Fire()
    {
        if(Time.time > nextFire && LevelManager.canMove)
        {
            nextFire = Time.time + fireRate;
            BulletSpawn();
        }
    }


}