using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // REFACTOR > input, movement, animation

    public CharacterStats characterStats;

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private PlayerControls _playerControls;

    private PlayerSkill _playerSkill;
    private PlayerUlt _playerUlt;
    private PlayerDash _playerDash;
    private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;

    [SerializeField] private InputActionReference _playerInputAction;


    [Header("Ground")]
    [SerializeField] private LayerMask jumpableGround;

    #region BaseStats

    [Header("Base Stats")]
    public float baseHealth = 22000f;
    public float baseAttack = 10;
    public float baseAttackPercent = 0;
    public float baseAttackFlat = 0;
    public float baseDamageBonus = 0;
    public float baseCritRate = 5;
    public float baseCritDamage = 50;
    public float baseDefense = 15;
    public float baseAttackSpeed = 5;

    #endregion


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _playerSkill = GetComponent<PlayerSkill>();
        _playerUlt = GetComponent<PlayerUlt>();
        _playerDash = GetComponent<PlayerDash>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAttack = GetComponent<PlayerAttack>();

        //audioManager = GetComponent<AudioManager>(); // doesnt work because component is not applied to this game object
        characterStats = new CharacterStats(baseHealth, baseAttack, baseAttackPercent, baseAttackFlat, baseDamageBonus, baseCritRate, baseCritDamage, baseDefense, baseAttackSpeed);
        Debug.Log("Player init");

        _playerControls = new PlayerControls();

    }
    void Start()
    {


        //UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
        _playerHealth.UpdateUI();

        // handling input through events
        //playerControls.Player.Jump.performed += _ => Jump2();
        //ReadInput();
    }

    private void ReadInput()
    {
        // list of player inputs with events being subscribed and calling the functions to perform the action using the start method
        _playerControls.Player.Jump.performed += context => Jump3(context.ReadValue<float>());
    }

    private void Jump2()
    {
        Debug.Log("Handling input with events calling the methods instead of having the methods in the update function");
    }

    private void Jump3(float jumpForce)
    {
        Debug.Log("Using the context to pass a value ");
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerInputAction.action.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerInputAction.action.Disable();
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        //if (isDashing) return;

        if (_playerHealth.IsAlive)
        {
            ProcessInput();
            _playerAttack.PerformAttack();
            _playerSkill.PerformSkill();
            _playerUlt.PerformUlt();
            _playerHealth.UpdateUI();
        }
    }

    private void FixedUpdate()
    {
        if (_playerDash.isDashing) return;

        if (_playerHealth.IsAlive)
        {
            Move();
            Jump();
            BetterJump();
            _playerDash.DashTrigger();
            Flip();
        }

        UpdateTimers();
        UpdateAnimationState();

    }

    #region Input

    public float damageAmount;
    public float healAmount;

    private void ProcessInput()
    {
        // horizontal movement
        //moveX = Input.GetAxisRaw("Horizontal");

        // reset jump counter
        if (IsGrounded() && !_playerControls.Player.Jump.triggered)
        {
            jumpCounter = 0f;
            IsJumpingMidAir = false;
            maxJump = defaultMaxJump;
        }

        // grounded 
        if (IsGrounded())
        {
            hangTimeCounter = hangTime;
        }
        else
        {
            hangTimeCounter -= Time.deltaTime;
        }

        // jump
        if (_playerControls.Player.Jump.triggered)
        {
            jumpBufferCounter = jumpBufferLength;

            if (jumpBufferCounter > 0f && (hangTimeCounter > 0f || jumpCounter < maxJump))
            {
                jumpRequest = true;
                jumpCounter++;
            }
        }
        else
        {
            if (jumpBufferCounter > -2f)
            {
                jumpBufferCounter -= Time.deltaTime;

            }
        }

        // attack
        if (_playerControls.Player.Attack.triggered)
        {
            _playerAttack.attackRequest = true;
        }

        // skill
        if (_playerControls.Player.Skill.triggered)
        {
            _playerSkill.skillAttackRequest = true;
        }

        // ult
        if (_playerControls.Player.Ult.triggered)
        {
            _playerUlt.ultAttackRequest = true;
        }

        // dash
        if (_playerControls.Player.Dash.triggered && _playerDash.canDash)
        {
            _playerDash.dashRequest = true;
        }

        // damage test DELETE
        if (Input.GetKeyDown(KeyCode.U))
        {
            _playerHealth.TakeDamage(damageAmount);
        }

        // heal test DELETE
        if (Input.GetKeyDown(KeyCode.I))
        {
            _playerHealth.Heal(healAmount);
        }
    }

    #endregion

    #region Timers
    private void UpdateTimers()
    {

        _playerHealth.UpdateHitTimer();

        _playerAttack.UpdateAttackTimer();

        _playerSkill.UpdateSkillTimer();
        _playerUlt.UpdateUltimer();
    }
    #endregion



    #region Movement

    [Header("Movement")]
    public float moveSpeed = 5f;
    private float moveX;

    private Vector2 moveH;
    private Vector2 direction;

    public bool isFacingRight = true;

    private void MoveOld()
    {
        _rb.velocity = new Vector2(moveX * moveSpeed, _rb.velocity.y);
    }

    private void Move()
    {
        moveH = _playerControls.Player.Move.ReadValue<Vector2>();

        direction = new Vector2(moveH.x * moveSpeed, _rb.velocity.y);

        if (direction != Vector2.zero)
        {
            _rb.velocity = direction;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    #endregion

    #region Jump

    [Header("Jump")]
    public float jumpForce = 5f;
    public float tapJumpMultiplier = 1f;
    public float holdJumpMultiplier = 1f;
    public float fallMultiplier = 2.5f;
    [Space]
    public float jumpCounter = 0f;
    public float maxJump = 2f;
    public float defaultMaxJump = 2f;
    [Space]
    // coyote jump
    public float hangTime = 0.2f;
    public float hangTimeCounter;
    [Space]
    // jump buffer
    public float jumpBufferLength = 0.2f;
    public float jumpBufferCounter;
    [Space]
    public bool IsJumpingMidAir = false;

    private bool jumpRequest = false;

    private void Jump()
    {
        if (jumpRequest)
        {
            FindObjectOfType<AudioManager>().PlaySound("Jump");

            if (IsJumpingMidAir)
            {
                // changing velocity to jump (could also do += Vector2.up * jumpForce;)
                _rb.velocity = Vector2.up * jumpForce;
                IsJumpingMidAir = false;
            }
            else
            {
                // adding force to jump (less prone to bug)
                //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _rb.velocity = Vector2.up * jumpForce;

            }

            hangTimeCounter = 0f;
            jumpBufferCounter = 0f;
            jumpRequest = false;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player taking damage is broke in the enemy scripts");
    }

    private void BetterJump()
    {
        // changing gravity directly

        if (_rb.velocity.y < 0f)
        {
            _rb.gravityScale = fallMultiplier;
        }

        _playerInputAction.action.canceled += context =>
        {
            if (_rb == null) return;

            if (_rb.velocity.y > 0)
            {
                _rb.gravityScale = tapJumpMultiplier;
            }

        };
        _playerInputAction.action.performed += context =>
        {
            if (_rb == null) return;
            _rb.gravityScale = holdJumpMultiplier;
        };

        /* OLD
          
         
        // changing gravity directly
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        --- velocity instead of gravity

        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        */
    }
    public void JumpPlayerInputComponent(InputAction.CallbackContext context)
    {
        /*
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        if (context.performed && IsGrounded())
        {
            rb.gravityScale = 1f;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            rb.gravityScale = tapJumpMultiplier;
        }*/
    }

    #endregion Jump


    private void Flip()
    {
        if (isFacingRight && moveH.x < 0f || !isFacingRight && moveH.x > 0f)
        {
            // flipping the player using scale

            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;

            FlipPlayerFirePoints();
        }

    }

    private void FlipPlayerFirePoints()
    {
        _playerSkill.Firepoint.Rotate(_playerSkill.Firepoint.rotation.x, 180f, _playerSkill.Firepoint.rotation.z);
        _playerSkill.SpawnPoint.Rotate(_playerSkill.Firepoint.rotation.x, 180f, _playerSkill.Firepoint.rotation.z);
        _playerUlt.UltSpawnPoint.Rotate(_playerSkill.Firepoint.rotation.x, 180f, _playerSkill.Firepoint.rotation.z);
    }

    #region Animation

    private Animator _animator;
    private string currentAnimation;

    private const string DerildoDeath = "derildo_death";
    private const string DerildoHit = "derildo_hit";
    private const string DerildoAttackString01 = "derildo_attack1";
    private const string DerildoAttackString02 = "derildo_attack2";
    private const string DerildoAttackString03 = "derildo_attack3";
    private const string DerildoSkill = "derildo_skill";
    private const string DerildoUlt = "derildo_ult";
    private const string DerildoJump = "derildo_jump";
    private const string DerildoFall = "derildo_fall";
    private const string DerildoWalk = "derildo_walk";
    private const string DerildoIdle = "derildo_idle";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        // death
        if (!_playerHealth.IsAlive)
        {
            ChangeAnimationState(DerildoDeath);
        }
        // hit
        else if (_playerHealth.IsHit)
        {
            ChangeAnimationState(DerildoHit);
        }
        // attack
        else if (_playerAttack.AttackAnimation)
        {
            if (_playerAttack.CurrentAttack == 1)
            {
                ChangeAnimationState(DerildoAttackString01);
                Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (_playerAttack.CurrentAttack == 2)
            {
                ChangeAnimationState(DerildoAttackString02);
                Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (_playerAttack.CurrentAttack == 3)
            {
                ChangeAnimationState(DerildoAttackString03);
                Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
        }
        // skill
        else if (_playerSkill.skillAttackAnimation)
        {
            ChangeAnimationState(DerildoSkill);

        }
        // ult
        else if (_playerUlt.ultAttackAnimation)
        {
            ChangeAnimationState(DerildoUlt);
        }
        // jump
        else if (_rb.velocity.y > .1f && !IsGrounded())
        {
            ChangeAnimationState(DerildoJump);
        }
        // fall
        else if (_rb.velocity.y < .1f && !IsGrounded())
        {
            ChangeAnimationState(DerildoFall);
        }
        // move
        else if (moveH.x > 0 || moveH.x < 0)
        {
            ChangeAnimationState(DerildoWalk);
        }
        // idle
        else
        {
            ChangeAnimationState(DerildoIdle);
        }

    }
    #endregion



    private bool IsGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, extraHeightText, jumpableGround);

        // draw gizmos
        /*

        Color rayColor;

        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);
        Debug.Log(raycastHit.collider);

         */

        return raycastHit.collider != null;

    }

}
