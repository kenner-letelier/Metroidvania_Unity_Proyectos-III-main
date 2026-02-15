using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour, ITargetCombat
{

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public GameObject player;

    [Header("Power Up")]
    [SerializeField] private PowerUpId _currentPowerUp;

    [SerializeField]
    private PowerUpId currentPowerUp
    {
        get
        {
            return _currentPowerUp;
        }
        set
        {
            if (_currentPowerUp != value)
            {
                GameManager.instance.UpdatePowerUp(value);
            }
            _currentPowerUp = value;
        }
    }

    [SerializeField] private int _powerUpAmount;

    [SerializeField]
    private int powerUpAmount
    {
        get
        {
            return _powerUpAmount;
        }

        set
        {
            if (value != _powerUpAmount)
            {
                GameManager.instance.UpdatePowerUp(value);
            }
            _powerUpAmount = value;
        }
    }
    [SerializeField]
    SpellLauncherController bluePotionLauncher;
    [SerializeField]
    SpellLauncherController redPotionLauncher;


    [Header("Health Variables")]

    [SerializeField] int _health = 0;

    [SerializeField]
    int health
    {
        get
        {
            return _health;
        }
        set
        {
            if (_health != value)
            {
                GameManager.instance.UpdateHealth(value);
            }
            _health = value;
        }
    }
    [SerializeField]
    DamageFeedbackEffect damageFeedbackEffect;
    [Header("Attack Variables")]
    [SerializeField]
    SwordController swordController;

    [Header("Animation Variables")]
    [SerializeField] AnimatorController animatorController;

    [Header("Checker Variables")]
    [SerializeField]
    LayerChecker footA;
    [SerializeField]
    LayerChecker footB;

    [Header("Boolean Variables")]
    public bool playerIsAttacking;
    public bool playerIsUsingPowerUp;
    public bool playerIsRecovering;
    public bool canDoubleJump;
    public bool isLookingRight;
    public bool isDead;

    [Header("Interruption Variables")]
    public bool canCheckGround;
    public bool canMove;

    public bool canFlip;

    [Header("Rigid Variables")]
    [SerializeField] private float damageForce;
    [SerializeField] private float damageForceUp;

    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;

    [SerializeField] private float speed;


    [Header("Audio")]
    [SerializeField] AudioClip attackSfx;
    [SerializeField] AudioClip jumpSfx;
    [SerializeField] AudioClip hurtSfx;


    //Control Variables
    [SerializeField] private Vector2 movementDirection;
    private bool jumpPressed = false;
    private bool attackPressed = false;
    private bool usePowerUpPressed = false;
    private bool died;
    private int _coins = -1;

    private bool isControlable = true;
    private int coins
    {
        get
        {
            return _coins;
        }
        set
        {
            if (_coins != value)
            {
                GameManager.instance.UpdateCoins(value);
            }

            _coins = value;
        }
    }

    public bool playerIsOnGround;


    private Rigidbody2D rigidbody2D;


    public static HeroController _instance;

    public static HeroController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HeroController>();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        

        SaveSystem.instance.Load(out HeroData heroData);
        if (heroData != null)
        {
            coins = heroData.coins;
            currentPowerUp = heroData.currentPowerUpId;
            powerUpAmount = heroData.powerUpAmount;


        }

        isDead = false;
        health = 10;
        coins = 0;
    }

    void Start()
    {

        

        canCheckGround = true;
        canMove = true;
        canFlip = true;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animatorController.Play(AnimationId.Idle);
        LoadPos();
        Temporizador.instance.IniciarTiempo();
    }


    void Update()
    {
        if (!died && isControlable)
        {
            HandleIsGrounding();
            HandleControls();
            HandleMovement();
            HandleFlip();
            HandleJump();
            HandleAttack();
            HandleUsePowerUp();
        }

        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadPos();
        }
    }
    public void savePosition()
    {
        var xPos = player.transform.position.x;
        var yPos = player.transform.position.y;
        PlayerPrefs.SetFloat("X", xPos);
        PlayerPrefs.SetFloat("Y", yPos);
        PlayerPrefs.Save();
        Debug.Log("posision GUARDADA");

    }

    public void LoadPos()
    {
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"));
    }

    public void SetIsControlable(bool isControlable)
    {

        this.isControlable = isControlable;
        if (!this.isControlable)
        {
            StopAllCoroutines();
            animatorController.Play(AnimationId.Idle);
            rigidbody2D.velocity = Vector2.zero;
        }
    }
    public void GiveCoin()
    {
        coins = Mathf.Clamp(coins + 1, 0, 10000000);
    }
    public void GiveHealthPoint()
    {
        health = Mathf.Clamp(health + 1, 0, 10);
    }
    public void ChangePowerUp(PowerUpId powerUpId, int amount)
    {
        currentPowerUp = powerUpId;
        powerUpAmount = amount;
        Debug.Log(currentPowerUp);
    }
    void HandleIsGrounding()
    {
        if (!canCheckGround) return;
        playerIsOnGround = footA.isTouching || footB.isTouching;
    }

    void HandleControls()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");
        attackPressed = Input.GetButtonDown("Attack");
        usePowerUpPressed = Input.GetButtonDown("UsePowerUp");
    }

    void HandleMovement()
    {
        if (!canMove) return;

        rigidbody2D.velocity = new Vector2(movementDirection.x * speed, rigidbody2D.velocity.y);


        if (playerIsOnGround)
        {
            if (Mathf.Abs(rigidbody2D.velocity.x) > 0)
            {
                animatorController.Play(AnimationId.Run);

            }
            else
            {
                if (Mathf.Abs(rigidbody2D.velocity.y) == 0)
                    animatorController.Play(AnimationId.Idle);


            }
        }


    }

    void HandleFlip()
    {
        if (!canFlip) return;

        if (movementDirection.magnitude > 0)
        {


            if (movementDirection.x >= 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                isLookingRight = true;
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                isLookingRight = false;
            }
        }

    }


    void HandleJump()
    {
        if (canDoubleJump && jumpPressed && !playerIsOnGround)
        {
            this.rigidbody2D.velocity = Vector2.zero;
            this.rigidbody2D.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

        if (jumpPressed && playerIsOnGround)
        {
            AudioManager.instance.PlaySfx(jumpSfx);
            this.rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            StartCoroutine(HandleJumpAnimation());
            canDoubleJump = true;
        }
        if (rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;


        }
        else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void HandleAttack()
    {
        if (attackPressed && !playerIsAttacking)
        {
            if (playerIsOnGround)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            AudioManager.instance.PlaySfx(attackSfx);
            animatorController.Play(AnimationId.Attack);
            playerIsAttacking = true;
            swordController.Attack(0.1f, 0.31f);

            StartCoroutine(RestoreAttack());
        }
    }

    IEnumerator RestoreAttack()
    {
        if (playerIsOnGround)
            canMove = false;
        yield return new WaitForSeconds(0.4f);
        playerIsAttacking = false;
        if (!playerIsOnGround)
            animatorController.Play(AnimationId.Jump);
        canMove = true;


    }



    public void PutOutBoundaries()
    {
        canFlip = false;
        this.transform.position = new Vector3(-15, 0, 0);
        rigidbody2D.velocity = Vector2.zero;
    }
    public void PutOnSpawnPosition(Vector2 position)
    {
        canFlip = true;
        this.transform.position = position;
    }

    public void UpdatePosition(Vector2 position)
    {
        this.transform.position = position;
        rigidbody2D.velocity = Vector2.zero;

    }
   

    void HandleUsePowerUp()
    {
        if (usePowerUpPressed && !playerIsUsingPowerUp && currentPowerUp != PowerUpId.Nothing)
        {
            if (playerIsOnGround)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            AudioManager.instance.PlaySfx(attackSfx);
            animatorController.Play(AnimationId.UsePowerUp);
            playerIsUsingPowerUp = true;



            swordController.Attack(0.1f, 0.31f);

            if (currentPowerUp == PowerUpId.BluePotion)
            {
                bluePotionLauncher.Launch((Vector2)transform.right + Vector2.up * 0.3f);

            }
            if (currentPowerUp == PowerUpId.RedPotion)
            {
                redPotionLauncher.Launch(transform.right);

            }

            StartCoroutine(RestoreUsePowerUp());

            powerUpAmount--;

            if (powerUpAmount <= 0)
            {
                currentPowerUp = PowerUpId.Nothing;
            }
        }
    }

    IEnumerator RestoreUsePowerUp()
    {
        if (playerIsOnGround)
            canMove = false;
        yield return new WaitForSeconds(0.4f);
        playerIsUsingPowerUp = false;
        if (!playerIsOnGround)
            animatorController.Play(AnimationId.Jump);
        canMove = true;


    }
    IEnumerator HandleJumpAnimation()
    {
        canCheckGround = false;
        playerIsOnGround = false;
        if (!playerIsAttacking)
            animatorController.Play(AnimationId.PrepareJump);
        yield return new WaitForSeconds(0.3f);
        if (!playerIsAttacking)
            animatorController.Play(AnimationId.Jump);
        canCheckGround = true;

    }

    public void TakeDamage(int damagePoints)
    {
        if (!playerIsRecovering && !died)
        {

            AudioManager.instance.PlaySfx(hurtSfx);

            health = Mathf.Clamp(health - damagePoints, 0, 10);
            if (health <= 0)
            {
                died = true;
                isDead = true;
            }
            StartCoroutine(StartPlayerRecover());
            if (isLookingRight)
            {
                rigidbody2D.AddForce(Vector2.left * damageForce + Vector2.up * damageForceUp, ForceMode2D.Impulse);

            }
            else
            {
                rigidbody2D.AddForce(Vector2.right * damageForce + Vector2.up * damageForceUp, ForceMode2D.Impulse);

            }
        }
    }

    IEnumerator StartPlayerRecover()
    {
        canMove = false;
        canFlip = false;
        animatorController.Play(AnimationId.Hurt);
        yield return new WaitForSeconds(0.2f);
        canMove = true;
        canFlip = true;
        rigidbody2D.velocity = Vector2.zero;

        playerIsRecovering = true;
        damageFeedbackEffect.PlayBlinkDamageEffect();
        yield return new WaitForSeconds(2);
        damageFeedbackEffect.StopBlinkDamageEffect();
        playerIsRecovering = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CheckPoint"))
        {
            savePosition();
        }

    }
}
