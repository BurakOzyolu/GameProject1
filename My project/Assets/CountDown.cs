using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float duration;
    public Image cooldownImage;

    private void Start()
    {
        cooldownImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    public void Timer()
    {
        if (Movement.dashed)
        {
            duration -=Time.deltaTime;
            cooldownImage.fillAmount = Mathf.InverseLerp(0.5f, 0, duration);
        }
        else
        {
            cooldownImage.fillAmount = 0f;
            duration = 0.5f;
        }
    }
}
