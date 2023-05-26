using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region Singleton
    public static Movement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion

    static private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed;
    [SerializeField] float playerYBoundry;
    Delay delay;
    PlayerHealth playerHealth;
    private float horizontalMove;
    TrailRenderer tr;
    private Jump jump;
    private Animator animator;

    public static bool canDash = true;
    public static bool isDashing = false;
    public static bool dashed;
    public static bool blocking;
    public static bool died;

    [SerializeField] float dashAmount;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTime;

    void Start()
    {
        Cancel();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        delay = GameObject.Find("Level Manager").GetComponent<Delay>();
        playerHealth = GameObject.Find("Level Manager").GetComponent<PlayerHealth>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();  
        jump = GetComponent<Jump>(); 
    }

    // Surekli olarak karakterimiz hareket edecegi icin veya ne zaman asagiya duserek olecegini bilemedigimiz icin bu iki metodu Update icerisinde calistiriyoruz.
    void Update()
    {
        MovementAction();
        PlayerDestroyer();
        StartDash();
        Block();
    }

    // Karakterimizin hareket etmesini saglayan metod. Once saga veya sola gitme duruma bagli olarak horizontalMove degiskenine -1 veya +1 atiyoruz. Bunu kontrol etmek icin ise Input.GetAxis("Horizontal") ile sagliyoruz. Horizontal yazmamizin sebebi su Unity bizim icin kolaylik saglamis. A veya sol ok basma durumunda -1, D veya sag ok basma durumunda +1 degeri donduruyor. Bizde bu duruma gore rigidBody2D ye velecotiy hiz vererek hareket etmesini sagliyoruz. Daha sonrasinda ise karakterimiz hangi yone bakmasi gerekiyor ise o yone dogru Flipleme (dondurme) islemini gerceklestiriyoruz.
    void MovementAction()
    {
        if (isDashing)
        {
            return;
        }

        if (LevelManager.canMove)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
            SpriteFlip(horizontalMove);
            animator.SetFloat("Move", Mathf.Abs(horizontalMove));
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Karakterimizin sahip oldugu spriteRenderer icerisinde karakterimizi otomatik dondurmemizi saglayan .flipX metodu bulunuyor. Karakterimizin sola dogru gidiyorsa .flipX cevirerek karakterin donmesini sagliyoruz. Eger saga gidiyorsa donmesini iptal ediyoruz. SpriteFlip metodu karakterin hareket ettigi yone dogru bakmasini saglayan metod.
    void SpriteFlip(float horizontalMove)
    {
        if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Karakterimiz asagiya duserek olebilir. PlayerDestroyer metodunda bu olayi gerceklestiriyoruz.
    void PlayerDestroyer()
    {
        if (transform.position.y < playerYBoundry)
        {
            SoundManager.instance.PlayWithIndex(3);
            Destroy(gameObject);
            Movement.Cancel();
            playerHealth.Lives();

            if (delay.delayTime == true)
            {
                delay.StartDelayTime();
            }
        }
    }

    void StartDash()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && horizontalMove != 0)
        {
            StartCoroutine(Dash());
            SoundManager.instance.PlayWithIndex(14);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        Jump.fallGravityScale = 0f;
        tr.emitting = true;
        rb.velocity = new Vector2(dashAmount * horizontalMove,0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 1f;
        Jump.fallGravityScale = 15f;
        tr.emitting = false;
        isDashing = false;
        dashed = true;
        yield return new WaitForSeconds(dashCooldown);
        dashed = false;
        canDash = true;
    }

    public static void Cancel()
    {
        canDash = true;
        isDashing = false;
        dashed = false;
        Jump.fallGravityScale = 15f;
        LevelManager.canMove = true;
        died = false;
    }
    //public void DieAnimation()
    //{
    //    animator.SetBool("Die", true);
    //}
    public void Die()
    {
        Destroy(gameObject);
        Died();
        PlayerHealth.instance.Lives();
        Cancel();
        if (Delay.instance.delayTime)
        {
            Delay.instance.StartDelayTime();
        }
    }
    void Block()
    {
        if (Input.GetMouseButton(0) && jump.IsGrounded() && !died)
        {
            animator.SetBool("Shield", true);
            blocking = true;
            rb.velocity = Vector2.zero;
            LevelManager.canMove = false;
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            animator.SetBool("Shield", false);
            blocking = false;
            LevelManager.canMove = true;
        }
        else if (!jump.IsGrounded())
        {
            animator.SetBool("Shield", false);
        }
    }
    public void Died()
    {
        died = true;
    }
}
