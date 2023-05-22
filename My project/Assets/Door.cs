using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject runText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winPanel.SetActive(true);
            LevelManager.canMove = false;
            runText.SetActive(false);
            SoundManager.instance.PlayWithIndex(13);
           
        }
    }
}
