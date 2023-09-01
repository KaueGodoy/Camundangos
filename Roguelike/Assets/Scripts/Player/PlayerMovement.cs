using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _playerInputAction;
    private PlayerControls _playerControls;
    private Rigidbody2D _rb;

    public Rigidbody2D Rigidbody { get { return _rb; } }

    private AudioManager _audioManager;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
        _playerControls = new PlayerControls();
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

    #region Movement

    [Header("Movement")]
    public float moveSpeed = 5f;

    public Vector2 moveH;
    public Vector2 direction;

    public void Move()
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

    public bool jumpRequest = false;

    public void Jump()
    {
        if (jumpRequest)
        {
            _audioManager.PlaySound("Jump");

            if (IsJumpingMidAir)
            {
                _rb.velocity = Vector2.up * jumpForce;
                IsJumpingMidAir = false;
            }
            else
            {
                _rb.velocity = Vector2.up * jumpForce;
            }

            hangTimeCounter = 0f;
            jumpBufferCounter = 0f;
            jumpRequest = false;

            HandleHoldJump();

        }
    }

    private void HandleHoldJump()
    {
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

    }

    #endregion Jump

    #region Grounded

    [Header("Ground")]
    [SerializeField] private LayerMask _jumpableGround;
    private BoxCollider2D _boxCollider;

    public bool IsGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center,
                                                    _boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down,
                                                    extraHeightText, _jumpableGround);

        return raycastHit.collider != null;
    }

    #endregion

   
}
