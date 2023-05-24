using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour

    //Buradaki script zeminleri kontrol etmeye yariyor. Sahnemiz icerisinde Ground tagini verdigimiz spritelar bulunuyor. Hatirlarsaniz ki player prefabi icerisinde biz feetPos adinda bir sey olusturmustuk ve biz bu feetPos'un triggerini acmistik. Asagidaki OnTriggerEnter2D icerisinde diyoruz ki feetPos eger tagi "Ground" olan bir nesne ile carpisir ise bizim SoundManager scripti icerisindeki zemin sesini cikarmamizi saglayan .LandSoundSound() adindaki metodunu cagir. 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlayWithIndex(10);
        }
    }
}
