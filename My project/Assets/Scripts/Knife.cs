using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] ParticleSystem particle;
    [SerializeField] float destroyLimit;
    private Rigidbody2D rb;
    [Header("Mode Speed")]
    [SerializeField] float easySpeed;
    [SerializeField] float normalSpeed;
    [SerializeField] float hardSpeed;
    private GameObject player;
    void Start()
    {
        moveSpeed = HardenedScript.instance.HardenedLevel(moveSpeed, easySpeed, normalSpeed, hardSpeed);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { Destroy(gameObject); }
        if (transform.position.x < -destroyLimit)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(-transform.right * turnSpeed);
        rb.velocity = Vector2.left * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayWithIndex(9);
            Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            PlayerHealth.instance.Lives();
            if (Delay.instance.delayTime)
            {
                Delay.instance.StartDelayTime();
            }
            Movement.Cancel(); 
            Destroy(gameObject);
        }
    }
}
