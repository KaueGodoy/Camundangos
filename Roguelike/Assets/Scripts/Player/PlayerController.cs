using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // REFACTOR > input, movement, animation, stats
    [Header("Mobile")]
    [SerializeField] private GameObject _mobileUI;
    private bool isMobileUIActive;

    [Header("Camera")]
    [SerializeField] private CameraFollowObject _cameraFollowObject;
    private float _fallSpeedYDampingChangeThreshold;

    private PlayerControls _playerControls;

    private PlayerDash _playerDash;
    private PlayerMovement _playerMovement;
    private HandlePlayerAnimation _playerAnimation;

    [Header("Dependencies")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        //_playerAttack = GetComponent<PlayerAttack>();
        //_playerSkill = GetComponent<PlayerSkill>();
        //_playerUlt = GetComponent<PlayerUlt>();
        _playerDash = GetComponent<PlayerDash>();
        _playerAnimation = GetComponent<HandlePlayerAnimation>();

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

        _fallSpeedYDampingChangeThreshold = CameraManager.Instance.FallSpeedYDampingChangeThreshold;
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

        if (PlayerHealth.IsAlive)
        {
            ProcessInput();
            _playerAttack.PerformAttack();
            //_playerSkill.PerformSkill();
            //_playerUlt.PerformUlt();
            _playerHealth.UpdatePlayerHealthBar();
        }

        if (_playerMovement.Rigidbody.velocity.y < _fallSpeedYDampingChangeThreshold
            && !CameraManager.Instance.IsLerpingYDamping
            && !CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpYDamping(true);
            //Debug.Log("Lerp true");

        }

        if (_playerMovement.Rigidbody.velocity.y >= 0f
            && !CameraManager.Instance.IsLerpingYDamping
            && CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpedFromPlayerFalling = false;
            CameraManager.Instance.LerpYDamping(false);
            //Debug.Log("Lerp false");

        }

    }

    private void FixedUpdate()
    {
        if (_playerDash.IsDashing) return;

        if (PlayerHealth.IsAlive)
        {
            _playerMovement.Move();
            _playerMovement.Jump();
            _playerDash.TriggerDash();
            FlipSprite();
        }

        UpdateTimers();
        //_playerAnimation.UpdateAnimationState();

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
            //_playerSkill.skillAttackRequest = true;
        }

        if (_playerControls.Player.Ult.triggered)
        {
            //_playerUlt.ultAttackRequest = true;
        }

        if (_playerControls.Player.Dash.triggered && _playerDash.CanDash)
        {
            _playerDash.DashRequest = true;
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

        if (_playerControls.UI.Mobile.triggered)
        {
            isMobileUIActive = !isMobileUIActive;
            _mobileUI.SetActive(isMobileUIActive);
        }

        //Debug.Log(CurrentRotation);
    }

    #endregion

    #region Timers
    private void UpdateTimers()
    {
        _playerHealth.UpdateHitTimer();
        _playerAttack.UpdateAttackTimer();
        //_playerSkill.UpdateSkillTimer();
        //_playerUlt.UpdateUltimer();
    }
    #endregion


    public bool IsFacingRight = true;
    public float CurrentRotation { get { return IsFacingRight ? 180f : 0f; } set { } }

    public void FlipSprite()
    {
        if (IsFacingRight && _playerMovement.MoveH.x < 0f || !IsFacingRight && _playerMovement.MoveH.x > 0f)
        {
            // flipping using scale

            //Vector3 localScale = transform.localScale;
            //IsFacingRight = !IsFacingRight;
            //localScale.x *= -1f;
            //transform.localScale = localScale;

            // flipping using rotation
            Vector3 rotator = new Vector3(transform.rotation.x, CurrentRotation, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _cameraFollowObject.TurnCamera();
            IsFacingRight = !IsFacingRight;

            //FlipPlayerFirePoints();
        }
    }

    //private void FlipPlayerFirePoints()
    //{
    //    _playerSkill.SkillSpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
    //    _playerSkill.SpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
    //    _playerUlt.UltSpawnPoint.Rotate(_playerSkill.SkillSpawnPoint.rotation.x, 180f, _playerSkill.SkillSpawnPoint.rotation.z);
    //}



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
        _playerHealth.TakeDamage(damage);
        Debug.Log("Player taking damage is broken in the enemy scripts");
        DamagePopup.Create(transform.position + Vector3.right + Vector3.up, (int)damage);
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
