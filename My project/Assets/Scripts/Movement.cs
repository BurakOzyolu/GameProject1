using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    static private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed;
    [SerializeField] float playerYBoundry;
    SoundManager soundManager;
    Delay delay;
    PlayerHealth playerHealth;
    private float horizontalMove;
    TrailRenderer tr;

    public static bool canDash = true;
    public static bool isDashing = false;
    public static bool dashed;

    [SerializeField] float dashAmount;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        delay = GameObject.Find("Level Manager").GetComponent<Delay>();
        playerHealth = GameObject.Find("Level Manager").GetComponent<PlayerHealth>();
        tr = GetComponent<TrailRenderer>();
    }

    // Surekli olarak karakterimiz hareket edecegi icin veya ne zaman asagiya duserek olecegini bilemedigimiz icin bu iki metodu Update icerisinde calistiriyoruz.
    void Update()
    {
        MovementAction();
        PlayerDestroyer();
        StartDash();
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
            soundManager.PlayWithIndex(3);
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
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("Dashing");
        canDash = false;
        isDashing = true;
        dashed = true;
        rb.gravityScale = 0f;
        Jump.fallGravityScale = 0f;
        rb.velocity = new Vector2(dashAmount * horizontalMove,0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 1f;
        Jump.fallGravityScale = 15f;
        isDashing = false;
        tr.emitting = true;
        yield return new WaitForSeconds(dashCooldown);
        dashed = false;
        Debug.Log("Can dash");
        canDash = true;
    }

    public static void Cancel()
    {
        canDash = true;
        isDashing = false;
        Jump.fallGravityScale = 15f;
    }
}