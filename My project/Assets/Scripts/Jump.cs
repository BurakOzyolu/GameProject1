using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpPower;
    [SerializeField] private float radius;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask layerMask;
    public static float gravityScale = 1.2f;
    public static float fallGravityScale = 7f;

    private Animator animator;

    [SerializeField] float startJumpTime;
    float jumpTime;
    bool isJumping;
    bool doubleJump;
    // Karakterimizin ziplamasini saglamak icin rigidBody2D, ziplama sesi cikarabilmesi icin ise SoundManager'i cekiyoruz.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        JumpAction();   
    }
    void JumpAction()
    {
        if (Movement.isDashing)
        {
            return;
        }
        //Eger Jump tusuna (space) basilir ise ve bizim karakterimiz yerde ise kodlari calistir diyoruz. 
        if (Input.GetButtonDown("Jump") && IsGrounded() && LevelManager.canMove)
        {
            // rigidBodymize kuvvet uygulayarak ziplama saglayabiliyoruz. .AddForce(1,2)
            // 1: Karakterimize hangi yonde ne kadar ziplatmak istiyorsak onun degeri,
            // 2:Kuvvet cesidi (2 tane kuvvet cesidi var biri Impulse digeri Force) [Impulse :Tum kuvveti bir anda veriyor][Force :Yavas veriyor kuvveti roketin kalkmasi gibi dusunebiliriz]
            //isJumping = true;
            doubleJump = true;
            jumpTime = startJumpTime;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
            SoundManager.instance.PlayWithIndex(8);
        }
        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            rb.AddForce(Vector2.up * jumpPower/3, ForceMode2D.Impulse);
            SoundManager.instance.PlayWithIndex(8);
            doubleJump = false;
        }
        //if (Input.GetButton("Jump") && isJumping)
        //{
        //    if (jumpTime > 0 )
        //    {
        //        rb.AddForce(Vector2.up, ForceMode2D.Impulse);
        //        jumpTime -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        isJumping = false;
        //    }
        //}
        //if (Input.GetButtonUp("Jump"))
        //{
        //    isJumping = false;
        //}
        if (Mathf.Approximately(rb.velocity.y,0))
        {
            animator.SetBool("Jump", false);
        }
        Gravity();
    }
    public bool IsGrounded()
    {
        // Physics2D.OverlapCircle 2D cember icerisindeki nesneleri kontrol etmek icin kullaniliyor. Sirasiyla cemberin merkez noktasini, yaricapini ve katmanini alir.
        // Burada bizim karakterimizin havada ziplamasini engellemek icin feetPos'un etrafinda olusan cemberin icerisinde Ground var mi yok mu ona bakiyor eger var ise true alir yok ise false degerini alir.
        return Physics2D.OverlapCircle(feetPos.position,radius,layerMask);
    }

    // Biraz daha gercekcilik katmak icin burada yer cekimi adinda bir metod olusturduk. Ziplarken ve yere duserkenki yer cekmini ayarliyoruz.
    void Gravity()
    {
        if(rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if(rb.velocity.y < 0){
            rb.gravityScale = fallGravityScale;
        }
    }
}
