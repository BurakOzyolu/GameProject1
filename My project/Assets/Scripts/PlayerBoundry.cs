using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundry : MonoBehaviour

    // Bu script karakterimizin sag ve sol taraftan oyun ekranindan cikmasini engelliyor. Asagida Update icerisinde bulunan if kosullarimiz su sekilde calisiyor. Eger karakterin pozisyonu bizim verdigimiz sinirlari gecmeye calisiyor ise scriptin bagli olan GameObjectin pozisyonunu bizim verdigimiz sekilde ayarla diyoruz.
{
    [SerializeField] float horizontalBoundry;
    float xMove;
    [SerializeField] float verticalBoundry;
    private void Update()
    {
        //if(transform.position.x < -horizontalBoundry)
        //{
        //    transform.position = new Vector3(-horizontalBoundry,transform.position.y,transform.position.z);
        //}
        //if (transform.position.x > horizontalBoundry)
        //{
        //    transform.position = new Vector3(horizontalBoundry, transform.position.y, transform.position.z);
        //}
        xMove = Mathf.Clamp(transform.position.x,-horizontalBoundry, horizontalBoundry);
        transform.position = new Vector2(xMove, transform.position.y);
        
        if (transform.position.y > verticalBoundry)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundry, transform.position.z);
        }
    }
}
