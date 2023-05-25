using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoTween : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    void Start()
    {
        levelText.text = "Level " + UIManager.level;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(new Vector3(2, 2, 1), .5f));
        mySequence.Append(transform.DOScale(new Vector3(0, 0, 1), .5f));
        mySequence.OnComplete(DestroyText);
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }
}
