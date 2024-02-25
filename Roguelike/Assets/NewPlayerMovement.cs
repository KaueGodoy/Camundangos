using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    public static NewPlayerMovement Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    public Vector2 MoveDir;

    [Header("Jump")]
    [SerializeField] private InputActionReference _playerInputAction;
    [SerializeField] private float _jumpForce = 5f;
    public float JumpForce { get { return _jumpForce; } }

    [SerializeField] private float _tapJumpMultiplier = 1f;
    public float TapJumpMultiplier { get { return _tapJumpMultiplier; } }

    [SerializeField] private float _holdJumpMultiplier = 1f;
    public float HoldJumpMultiplier { get { return _holdJumpMultiplier; } }

    [SerializeField] private float _fallMultiplier = 2.5f;
    public float FallMultiplier { get { return _fallMultiplier; } }
    [Space]
    public float jumpCounter = 0f;
    public float _baseJumpAmount = 2f;
    public float _currentJumpAmount = 1f;
    [Space]
    // coyote jump
    public float HangTime = 0.2f;
    public float HangTimeCounter;
    [Space]
    // jump buffer
    public float JumpBufferLength = 0.2f;
    public float JumpBufferCounter;
    [Space]
    public bool IsJumpingMidAir = false;
    public bool JumpRequest = false;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private bool _isDashing;

    private Coroutine _dashCoroutine;

    [Header("Ground")]
    [SerializeField] private LayerMask _jumpableGround;

    private BoxCollider2D _boxCollider;
    public Rigidbody2D Rb;

    public float CurrentRotation { get { return transform.rotation.y >= 0 ? 1 : -1; } set { } }

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
    }

    public void Move()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        MoveDir = new Vector2(inputVector.x * _moveSpeed, Rb.velocity.y);

        if (MoveDir != Vector2.zero)
        {
            Rb.velocity = MoveDir;
        }
        else
        {
            Rb.velocity = Vector3.zero;
        }
    }


    public void Jump()
    {
        if (!IsGrounded()) return;

        if (IsJumpingMidAir)
        {
            Rb.velocity = Vector2.up * JumpForce;
            IsJumpingMidAir = false;
        }
        else
        {
            Rb.velocity = Vector2.up * JumpForce;
        }

        HangTimeCounter = 0f;
        JumpBufferCounter = 0f;

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

        Rb.velocity = new Vector2(CurrentRotation * _dashSpeed, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        tr.emitting = false;
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

    private void HandleMovementOld()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * _moveSpeed;
    }

}
