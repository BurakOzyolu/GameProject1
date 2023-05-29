using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystem blockingParticle;
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
        if (player == null || CountManager.instance.EndCount()) { Destroy(gameObject); }
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
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && LevelManager.canMove && !Movement.blocking)
        {
            SoundManager.instance.PlayWithIndex(9);
            Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
            Animator anim = collision.gameObject.GetComponent<Animator>();
            LevelManager.canMove = false;
            anim.SetTrigger("Die");
            
        }
        else if(Movement.blocking)
        {
            Instantiate(blockingParticle, collision.gameObject.transform.position, Quaternion.identity);

        }
    }
}
