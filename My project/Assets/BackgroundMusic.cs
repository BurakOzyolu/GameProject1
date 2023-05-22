using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Buradaki if blogu su ise yariyor. Biz yukarida BackgroundMusic turunde bir degisken olusturduk ve icerisinde arkaplan muzigi tutmamizi saglayacak. Diyoruz ki eger herhangi bir arkaplan muzigi yoksa yani instance == null ise instance degiskenine mevcut olan arkaplan muzigi ata. Eger yoksa mevcut olan gameObjecti (arkaplan muzigi)'ni yok et. Cunku birden fazla arkaplan bulundugu anlamina geliyor.
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Sahnede birden fazla Background Music var.");
        }
        DontDestroyOnLoad(gameObject); 
        // DontDestroyOnLoad metodu icerisinde bulunan parametreyi/degiskeni baska bir sahneye gecse bile silmesini engelleyecek.
        // Buradaki gameObject objesi bizim Background muzigimiz oldugu icin level degisse bile oyunumuzdaki arkaplan muzigi silinmeyip devam edecek.
    }
    private void Update()
    {
        MuteMusic();
    }
    void MuteMusic()
    {
        if (PauseMenu.isPause)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
}
