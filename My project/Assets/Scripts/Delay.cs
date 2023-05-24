using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    LevelManager levelManager; // LevelManager scriptine ulasmak icin degisken 
    [SerializeField] float delayTimer; // Karakterimizin dogmasi icin gerekli sureyi ayarlamak icin degisken
    [SerializeField] public bool delayTime = true; // Karakterimizin cani >1 mi degil mi onu kontrol etmek icin bool tipinde degisken 

    #region Singleton
    public static Delay instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private void Start()
    {
        levelManager = GetComponent<LevelManager>(); // Burada GameObject.Find kullanmadik cunku Delay scriptimiz bizi halihazir LevelManager icerisinde oldugu icin direkt. GetComponent yaparak scripti cektik.
    }

    public void StartDelayTime() // Coroutini diger scriptlerde kullanip cagirabilmek icin olusturdugumuz bir metod.
    {
        StartCoroutine(DelayNewTime()); // Asagida yazdigimiz Coroutini calistirir. (Karakterin belirledigimiz sure sonrasi tekrar canlanmasini sagliyor.)
    }

    // Coroutine : Aslinda direkt metod (fonksiyon) ile ayni islevdedir. Fakat tek farki burada metod icerisinde bekletme islemi yapabilmemizi sagliyor. Ornegin; belirli araliklarda isik yandirip sondurmek, karakterin yeniden dogmasi icin sure bekletmek, silahin belirli araliklarla ateslenmesini saglamak gibi. Bekletme olayini gerceklestirmek icin bizim metodun donus tipi IEnumerator olmasi gerekiyor. IEnumerator tipi ile deger dondurdugumuz zaman sisteme "dur bakayim, su kadar sure bekleyeceksin" diyoruz. Coroutinleri calistirabilmek icin StartCoroutine adi verilen fonksiyonu kullanmamiz gerekiyor. Yukarida ornegi var. 
    IEnumerator DelayNewTime()
    {
        yield return new WaitForSeconds(delayTimer); //DelayTimer icerisine yazdigimiz sure kadar bekle bunu dondur diyoruz.
        levelManager.RespawnPlayer(); // LevelManager scripti icerisinden karakterimizi yeniden canlandiran metodu cagiriyoruz.
    }
}
