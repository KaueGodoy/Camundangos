using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // REFACTOR > input, movement, animation, stats

    public CharacterStats characterStats;

    private PlayerControls _playerControls;

    private PlayerSkill _playerSkill;
    private PlayerUlt _playerUlt;
    private PlayerDash _playerDash;
    private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;
    private PlayerMovement _playerMovement;
    private HandlePlayerAnimation _playerAnimation;

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
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerSkill = GetComponent<PlayerSkill>();
        _playerUlt = GetComponent<PlayerUlt>();
        _playerDash = GetComponent<PlayerDash>();
        _playerAnimation = GetComponent<HandlePlayerAnimation>();

        characterStats = new CharacterStats(baseHealth, baseAttack, baseAttackPercent, baseAttackFlat, baseDamageBonus, baseCritRate, baseCritDamage, baseDefense, baseAttackSpeed);
        Debug.Log("Player init");

        _playerControls = new PlayerControls();

    }

    void Start()
    {
        //UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
        _playerHealth.UpdatePlayerHealthBar();

        // handling input through events
        //playerControls.Player.Jump.performed += _ => Jump2();
        //ReadInput();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        if (_playerHealth.IsAlive)
        {
            ProcessInput();
            _playerAttack.PerformAttack();
            _playerSkill.PerformSkill();
            _playerUlt.PerformUlt();
            _playerHealth.UpdatePlayerHealthBar();
        }
    }

    private void FixedUpdate()
    {
        if (_playerDash.isDashing) return;

        if (_playerHealth.IsAlive)
        {
            _playerMovement.Move();
            _playerMovement.Jump();
            _playerDash.TriggerDash();
            FlipSprite();
        }

        UpdateTimers();
        _playerAnimation.UpdateAnimationState();

    }

    #region Input

    public float DamageAmount = 1f;
    public float HealAmount = 1f;

    private void ProcessInput()
    {
        // reset jump counter
        if (_playerMovement.IsGrounded() && !_playerControls.Player.Jump.triggered)
        {
            _playerMovement.jumpCounter = 0f;
            _playerMovement.IsJumpingMidAir = false;
            _playerMovement._currentJumpAmount = _playerMovement._baseJumpAmount;
        }

        if (_playerMovement.IsGrounded())
        {
            _playerMovement.HangTimeCounter = _playerMovement.HangTime;
        }
        else
        {
            _playerMovement.HangTimeCounter -= Time.deltaTime;
        }

        if (_playerControls.Player.Jump.triggered)
        {
            _playerMovement.JumpBufferCounter = _playerMovement.JumpBufferLength;

            if (_playerMovement.JumpBufferCounter > 0f && (_playerMovement.HangTimeCounter > 0f || _playerMovement.jumpCounter < _playerMovement._currentJumpAmount))
            {
                _playerMovement.JumpRequest = true;
                _playerMovement.jumpCounter++;
            }
        }
        else
        {
            if (_playerMovement.JumpBufferCounter > -2f)
            {
                _playerMovement.JumpBufferCounter -= Time.deltaTime;

            }
        }

        if (_playerControls.Player.Attack.triggered)
        {
            _playerAttack.attackRequest = true;
        }

        if (_playerControls.Player.Skill.triggered)
        {
            _playerSkill.skillAttackRequest = true;
        }

        if (_playerControls.Player.Ult.triggered)
        {
            _playerUlt.ultAttackRequest = true;
        }

        if (_playerControls.Player.Dash.triggered && _playerDash.canDash)
        {
            _playerDash.dashRequest = true;
        }

        // damage test DELETE
        if (Input.GetKeyDown(KeyCode.U))
        {
            _playerHealth.TakeDamage(DamageAmount);
        }

        // heal test DELETE
        if (Input.GetKeyDown(KeyCode.I))
        {
            _playerHealth.Heal(HealAmount);
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


    public bool IsFacingRight = true;

    public void FlipSprite()
    {
        if (IsFacingRight && _playerMovement.MoveH.x < 0f || !IsFacingRight && _playerMovement.MoveH.x > 0f)
        {
            // flipping the player using scale

            Vector3 localScale = transform.localScale;
            IsFacingRight = !IsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;

            FlipPlayerFirePoints();
        }
    }
    private void FlipPlayerFirePoints()
    {
        _playerSkill.SkillSpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
        _playerSkill.SpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
        _playerUlt.UltSpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
    }



    // DELETE

    private void IsGroundedDrawGizmos()
    {
        //Color rayColor;

        //if (raycastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);
        //Debug.Log(raycastHit.collider);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player taking damage is broken in the enemy scripts");
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

}
