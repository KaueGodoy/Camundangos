using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    public static NewPlayerMovement Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Vector2 _moveDir;

    [Header("Jump")]
    [SerializeField] private InputActionReference _playerInputAction;
    [SerializeField] private float _jumpForce = 5f;
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

    public float FallMultiplier { get { return _fallMultiplier; } }
    public float JumpForce { get { return _jumpForce; } }
    public float TapJumpMultiplier { get { return _tapJumpMultiplier; } }
    public float HoldJumpMultiplier { get { return _holdJumpMultiplier; } }

    [Header("Dash")]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private bool _isDashing;

    private Coroutine _dashCoroutine;

    [Header("Ground")]
    [SerializeField] private LayerMask _jumpableGround;

    [Header("Sprite")]
    [SerializeField] private bool _isFacingRight = true;

    public float CurrentRotation { get { return _isFacingRight ? 180f : 0f; } set { } }
    public float CurrentRotationDash { get { return transform.rotation.y >= 0 ? 1 : -1; } set { } }

    private BoxCollider2D _boxCollider;
    public Rigidbody2D Rb { get; private set; }

    private void Awake()
    {
        Instance = this;

        Rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerJump += GameInput_OnPlayerJump;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
    }

    private void GameInput_OnPlayerDash(object sender, System.EventArgs e)
    {
        Dash();
    }

    private void GameInput_OnPlayerJump(object sender, System.EventArgs e)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;

        Move();
        //FlipSprite();
    }

    public void Move()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        _moveDir = new Vector2(inputVector.x * _moveSpeed, Rb.velocity.y);

        if (_moveDir != Vector2.zero)
        {
            Rb.velocity = _moveDir;
            FlipSprite();
        }
        else
        {
            Rb.velocity = Vector3.zero;
        }
    }


    public void Jump()
    {
        if (!IsGrounded()) return;

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

        HandleHoldJump();
    }

    private void HandleHoldJump()
    {
        if (Rb.velocity.y < 0f)
        {
            Rb.gravityScale = FallMultiplier;
        }

        _playerInputAction.action.canceled += context =>
        {
            if (Rb == null) return;

            if (Rb.velocity.y > 0)
            {
                Rb.gravityScale = TapJumpMultiplier;
            }

        };
        _playerInputAction.action.performed += context =>
        {
            if (Rb == null) return;
            Rb.gravityScale = HoldJumpMultiplier;
        };

    }

    private void JumpOld()
    {
        if (!IsGrounded()) return;

        Rb.velocity = Vector2.up * _jumpForce;
    }

    private void Dash()
    {
        CallDashCoroutine();
    }

    private IEnumerator DashCoroutine()
    {
        // audio

        _isDashing = true;

        float originalGravity = Rb.gravityScale;
        Rb.gravityScale = 0f;

        Rb.velocity = new Vector2(CurrentRotationDash * _dashSpeed, 0f);
        _trailRenderer.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        _trailRenderer.emitting = false;
        Rb.gravityScale = originalGravity;

        _isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
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
        if (_isFacingRight && _moveDir.x < 0f ||
           !_isFacingRight && _moveDir.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, CurrentRotation, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            //_cameraFollowObject.TurnCamera();
            _isFacingRight = !_isFacingRight;
        }
    }

    private void HandleMovementOld()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * _moveSpeed;
    }

}
