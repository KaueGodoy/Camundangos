using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _playerInputAction;

    private PlayerControls _playerControls;

    private Rigidbody2D _rb;
    public Rigidbody2D Rigidbody { get { return _rb; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
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

    [SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed { get { return _moveSpeed; } }

    [SerializeField] private Vector2 _moveH;
    public Vector2 MoveH { get { return _moveH; } set { _moveH = value; } }

    [SerializeField] private Vector2 _direction;

    public void Move()
    {
        MoveH = _playerControls.Player.Move.ReadValue<Vector2>();

        _direction = new Vector2(MoveH.x * MoveSpeed, Rigidbody.velocity.y);

        if (_direction != Vector2.zero)
        {
            Rigidbody.velocity = _direction;
        }
        else
        {
            Rigidbody.velocity = Vector3.zero;
        }
    }

    public void MoveNew(Vector2 moveH)
    {
        _direction = new Vector2(moveH.x * MoveSpeed, Rigidbody.velocity.y);

        if (_direction != Vector2.zero)
        {
            Rigidbody.velocity = _direction;
        }
        else
        {
            Rigidbody.velocity = Vector3.zero;
        }
    }

    #endregion

    #region Jump

    [Header("Jump")]

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

    // IMPLEMENT LATER
    private float _jumpAmount;
    public float JumpAmount
    {
        get
        {
            _jumpAmount += _baseJumpAmount + _baseJumpAmount;

            return _jumpAmount;
        }
    }

    private int _value;
    public int DoubleValue { get { return _value * 2; } }

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

    public void Jump()
    {
        if (JumpRequest)
        {
            AudioManager.Instance.PlaySound("Jump");

            if (IsJumpingMidAir)
            {
                Rigidbody.velocity = Vector2.up * JumpForce;
                IsJumpingMidAir = false;
            }
            else
            {
                Rigidbody.velocity = Vector2.up * JumpForce;
            }

            HangTimeCounter = 0f;
            JumpBufferCounter = 0f;
            JumpRequest = false;

            HandleHoldJump();

        }
    }

    private void HandleHoldJump()
    {
        //if (Rigidbody.velocity.y < 0f)
        //{
        //    Rigidbody.gravityScale = FallMultiplier;
        //}

        _playerInputAction.action.canceled += context =>
        {
            if (Rigidbody == null) return;

            if (Rigidbody.velocity.y > 0)
            {
                Rigidbody.gravityScale = TapJumpMultiplier;
            }

        };
        _playerInputAction.action.performed += context =>
        {
            if (Rigidbody == null) return;
            Rigidbody.gravityScale = HoldJumpMultiplier;
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
