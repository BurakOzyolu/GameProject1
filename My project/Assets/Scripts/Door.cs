using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject runText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && LevelManager.canMove)
        {
            winPanel.SetActive(true);
            runText.SetActive(false);
            SoundManager.instance.PlayWithIndex(13);
            collision.gameObject.SetActive(false);
        }
    }
}
