using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    public static NewPlayerMovement Instance { get; private set; }

    [Header("Camera")]
    [SerializeField] private CameraFollowObject _cameraFollowObject;
    private float _fallSpeedYDampingChangeThreshold;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    public Vector2 MoveDirection { get; private set; }

    [Header("Jump")]
    [SerializeField] private InputActionReference _jumpInputAction;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _defaultJumpForce = 12f;
    [SerializeField] private float _jumpMultiplier = 1.2f;
    [SerializeField] private float _tapJumpMultiplier = 1f;
    [SerializeField] private float _holdJumpMultiplier = 1f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [Space]
    [SerializeField] private float _jumpCounter = 0f;
    [SerializeField] private float _baseJumpAmount = 2f;
    [SerializeField] private float _currentJumpAmount = 1f;
    [Space]
    [Tooltip("Coyote Jump")]
    [SerializeField] private float _hangTime = 0.2f;
    [SerializeField] private float _hangTimeCounter;
    [Space]
    [Tooltip("Jump buffer")]
    [SerializeField] private float _jumpBufferLength = 0.2f;
    [SerializeField] private float _jumpBufferCounter;
    [Space]
    [SerializeField] private bool _isJumpingMidAir = false;
    [SerializeField] private bool _jumpRequest = false;
    [SerializeField] private bool _canJump;

    public float FallMultiplier { get { return _fallMultiplier; } }
    public float JumpForce { get { return _jumpForce; } private set { _jumpForce = value; } }
    public float TapJumpMultiplier { get { return _tapJumpMultiplier; } }
    public float HoldJumpMultiplier { get { return _holdJumpMultiplier; } }

    [Header("Dash")]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private bool _isDashing;
    [SerializeField] private bool _dashRequest;
    [SerializeField] private bool _canDash = true;

    private Coroutine _dashCoroutine;

    [Header("Ground")]
    [SerializeField] private LayerMask _jumpableGround;

    [Header("Sprite")]
    [SerializeField] private bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; } private set { } }
    public float CurrentRotation { get { return _isFacingRight ? 180f : 0f; } set { } }
    public float CurrentRotationDash { get { return transform.rotation.y >= 0 ? 1 : -1; } set { } }

    private BoxCollider2D _boxCollider;

    public Rigidbody2D Rb { get; private set; }

    private void Awake()
    {
        Instance = this;

        Rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _fallSpeedYDampingChangeThreshold = CameraManager.Instance.FallSpeedYDampingChangeThreshold;
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerJump += GameInput_OnPlayerJump;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;

        _canDash = true;
    }

    private void GameInput_OnPlayerDash(object sender, System.EventArgs e)
    {
        Dash();
    }

    private void GameInput_OnPlayerJump(object sender, System.EventArgs e)
    {
        Jump();
    }

    private void Update()
    {
        ProcessMovementStates();
    }

    private void FixedUpdate()
    {
        HandleCameraLerp();

        if (_isDashing) return;
        Move();
    }

    private void ProcessMovementStates()
    {
        ProcessHangTime();
        ResetJump();
        HandleFalling();
    }

    private void ProcessHangTime()
    {
        if (IsGrounded())
        {
            _hangTimeCounter = _hangTime;
        }
        else
        {
            _hangTimeCounter -= Time.deltaTime;
        }
    }

    private void SetJumpSpeed()
    {
        if (_jumpCounter == 0f)
        {
            JumpForce = _defaultJumpForce * _jumpMultiplier;
        }
        else
        {
            JumpForce = _defaultJumpForce;
        }
    }

    private void HandleCameraLerp()
    {

        if (Rb.velocity.y < _fallSpeedYDampingChangeThreshold
            && !CameraManager.Instance.IsLerpingYDamping
            && !CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpYDamping(true);
            //Debug.Log("Lerp true");

        }

        if (Rb.velocity.y >= 0f
            && !CameraManager.Instance.IsLerpingYDamping
            && CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpedFromPlayerFalling = false;
            CameraManager.Instance.LerpYDamping(false);
            //Debug.Log("Lerp false");
        }
    }

    public Vector2 InputVector;

    public void Move()
    {
        InputVector = GameInput.Instance.GetMovementVectorNormalized();
        MoveDirection = new Vector2(InputVector.x * _moveSpeed, Rb.velocity.y);

        //NORMALIZED FIXED
        //_moveDir = _moveDir.normalized;

        if (MoveDirection != Vector2.zero)
        {
            Rb.velocity = MoveDirection;
            FlipSprite();
        }
        else
        {
            Rb.velocity = Vector3.zero;
        }
    }

    public void Jump()
    {
        _jumpRequest = true;

        ProcessJump();
        PerformJump();
    }

    private void ProcessJump()
    {
        if (_jumpRequest)
        {
            _jumpBufferCounter = _jumpBufferLength;

            SetJumpSpeed();

            if (_jumpBufferCounter > 0f && (_hangTimeCounter > 0f || _jumpCounter < _currentJumpAmount))
            {
                _canJump = true;
                _jumpCounter++;
            }
            else
            {
                _canJump = false;
            }
        }
        else
        {
            if (_jumpBufferCounter > -2f)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
        }
    }

    private void PerformJump()
    {
        if (_canJump)
        {
            if (_isJumpingMidAir)
            {
                Rb.velocity = Vector2.up * JumpForce;
                _isJumpingMidAir = false;
            }
            else
            {
                Rb.velocity = Vector2.up * JumpForce;
            }

            _hangTimeCounter = 0f;
            _jumpBufferCounter = 0f;

            _jumpRequest = false;
        }
    }

    private void HandleHoldJump()
    {
        _jumpInputAction.action.canceled += context =>
        {
            if (Rb == null) return;

            if (Rb.velocity.y > 0)
            {
                Rb.gravityScale = TapJumpMultiplier;
            }

        };
        _jumpInputAction.action.performed += context =>
        {
            if (Rb == null) return;
            Rb.gravityScale = HoldJumpMultiplier;
        };
    }

    private void HandleFalling()
    {
        if (Rb.velocity.y < 0f)
        {
            Rb.gravityScale = FallMultiplier;
        }
    }

    private void ResetJump()
    {
        if (!IsGrounded()) return;

        _jumpCounter = 0f;
        _isJumpingMidAir = false;
        _currentJumpAmount = _baseJumpAmount;
    }

    private void Dash()
    {
        if (_canDash)
        {
            _dashRequest = true;
            PerformDash();
        }
    }

    private void PerformDash()
    {
        if (_dashRequest)
        {
            CallDashCoroutine();
            _dashRequest = false;
        }
    }

    private IEnumerator DashCoroutine()
    {
        // audio
        _canDash = false;
        _isDashing = true;

        AudioManager.Instance.PlaySound("PlayerDash");

        float originalGravity = Rb.gravityScale;
        Rb.gravityScale = 0f;

        Rb.velocity = new Vector2(CurrentRotationDash * _dashSpeed, 0f);
        _trailRenderer.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        _trailRenderer.emitting = false;
        Rb.gravityScale = originalGravity;

        _isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

    private void CallDashCoroutine()
    {
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    public bool IsGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center,
                                                    _boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down,
                                                    extraHeightText, _jumpableGround);

        return raycastHit.collider != null;
    }

    public void FlipSprite()
    {
        if (_isFacingRight && MoveDirection.x < 0f ||
           !_isFacingRight && MoveDirection.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, CurrentRotation, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _cameraFollowObject.TurnCamera();
            _isFacingRight = !_isFacingRight;
        }
    }
}
