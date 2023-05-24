using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float enemyAttackSpeed;
    [SerializeField] float xBoundry;
    [SerializeField] float yBoundry;
    Delay delay;
    bool isAttacking;
    Canvas canvas;
    PlayerHealth playerHealth;

    // Start : Oyun basladigi sira calisan bir metoddur.
    // Buradaki Start metodunda SoundManager, Delay, Canvas ve PlayerHealth scriptlerini kullanmak icin gerekli atamalari yapiyoruz. (onlari cekiyoruz)
    private void Start()
    {
        delay = GameObject.Find("Level Manager").GetComponent<Delay>();
        canvas = GameObject.Find("UI Manager").GetComponent<Canvas>();
        playerHealth = GameObject.Find("Level Manager").GetComponent<PlayerHealth>();
    }

    // Update : Her karede bir kez cagirilir. Guncelleme islemleri yapilir. (surekli calisir diyebiliriz)
    // Buradaki Update metodunda EnemyDestroyer metodunu calistiriyoruz.
    private void Update()
    {
        EnemyDestroyer();
    }

    // FixedUpdate : Sabit bir zaman diliminde calisir, update ile neredeyse ayni isleve sahiptir. Tek farki Update farkli kisilerde farkli sayida calisabilirken, burada herkes icin ayni sayida calisir. Genelde fizik tabanli islevler burada cagirilir.
    // Buradaki FixedUpdate metodunda EnemyAttack metodunu calistiriyoruz.
    private void FixedUpdate()
    {
        EnemyAttack();
    }

    // EnemyDestroyer : Bizim oyun icerisinde enemylerimiz mevcut. Oyunumuzdaki enemylerin yok olmasini saglayan bir metod.
    void EnemyDestroyer()
    {
        // Buradaki if blogu su ise yariyor. Eger enemymizin x pozisyonu veya y pozisyonu bizim belirttigimiz degerlerden kucuk olursa enemyyi yok et diyoruz.
        if((transform.position.x < xBoundry) || (transform.position.y < yBoundry)) 
        {
            Destroy(gameObject);
        }
    }

    // EnemyAttack : Enemylerin saldirmasini saglayan bir metod.
    void EnemyAttack()
    {
        transform.Translate(-1 * enemyAttackSpeed * Time.deltaTime, 0, 0); //transform.Translate : Scriptin bagli oldugu nesneyi mevcut konumu ve yonune gore belirtilen degerde hareketini saglar. Kisacasi dusmani hareket ettirdik
        
        // Diyoruz ki eger enemy saldiriya gecmediyse icindeki kodlari cagir. En basta yukarida hatirlanacagi uzere isAttacking false olarak ayarlamistik. O yuzden enemy ilk canlandiginda bi metod dogal olarak calisacak.
        while (!isAttacking) 
        {
            SoundManager.instance.PlayWithIndex(0); // Enemynin saldiri sesini cikarmasini sagladik.
            isAttacking = true; // Surekli olarak ses cikarmasini istemedigimiz icin donguden cikmak icin isAttackingi true olarak ayarladik.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bizim enemyler bir yerlere carpabilir. Eger bizim karakterimize carpar ise ne olmasi gerekiyor. Karakterimiz olum sesi cikarticak, oldugu icin yok olacak, cani bir azalicak ve eger cani var ise tekrar canlanicak. Buradaki if blogu bu ise yariyor. 
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayWithIndex(2);
            Destroy(collision.gameObject);
            Movement.Cancel();
            playerHealth.Lives();
            if (delay.delayTime == true)
            {
                delay.StartDelayTime();
            }
        }
    }
}
