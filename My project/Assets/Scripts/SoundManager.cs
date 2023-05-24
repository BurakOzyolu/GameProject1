using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Ses Kaynaklarini atamak icin gerekli degiskenleri olusturduk.
    [SerializeField] AudioSource audioSource;
    public AudioClip[] soundList;
    #region Singleton
    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Sahnede fazladan sound manager olamaz");
        }
    }
    #endregion
    // Sesleri calistabilmek icin bir ses kaynagi lazim bundan dolayi Start metodu icerisinde gerekli olan audioSource'u cektik.
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //PlayWithIndex ile sesin index ile sesi çalýþtýrýyoruz
    public void PlayWithIndex(int index)
    {
        audioSource.PlayOneShot(soundList[index]);
    }
}
