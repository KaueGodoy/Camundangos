using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _tapJumpMultiplier = 1f;
    [SerializeField] private float _holdJumpMultiplier = 1f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _baseJumpAmount = 2f;
    [SerializeField] private float _jumpCounter = 0f;
    [SerializeField] private float _currentJumpAmount = 1f;
    [SerializeField] private float _jumpBufferLength = 0.2f;
    [SerializeField] private float _hangTime = 0.2f;

    private float _hangTimeCounter;
    private float _jumpBufferCounter;
    private const float _minJumpBuffer = -2f;
    private bool _isJumpingMidAir;

    private AudioManager _audioManager;
    private Rigidbody2D _rb;
    private PlayerInputHandler _inputHandler;
    private PlayerGroundCheck _playerGroundCheck;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _audioManager = FindObjectOfType<AudioManager>();
        _rb = GetComponent<Rigidbody2D>();
        _playerGroundCheck = GetComponent<PlayerGroundCheck>();
    }

    private void Update()
    {
        ResetJumpCounter();
        HandleHangTime();
        CheckForInput();
        HandleHoldJump();
    }

    private void CheckForInput()
    {
        if (_inputHandler.IsJumpPressed())
        {
            _jumpBufferCounter = _jumpBufferLength;

            if (_jumpBufferCounter > 0f && (_hangTimeCounter > 0f || _jumpCounter < _currentJumpAmount))
            {
                Jump();
            }
        }
        else if (_jumpBufferCounter > _minJumpBuffer)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void ResetJumpCounter()
    {
        if (_playerGroundCheck.GroundedCheck() && !_inputHandler.IsJumpPressed())
        {
            _jumpCounter = 0f;
            _isJumpingMidAir = false;
            _currentJumpAmount = _baseJumpAmount;
        }
    }

    private void Jump()
    {
        if (_inputHandler.IsJumpPressed())
        {
            _audioManager.PlaySound("Jump");
            
            _jumpCounter++;

            if (_isJumpingMidAir)
            {
                _rb.velocity = Vector2.up * _jumpForce;
                _isJumpingMidAir = false;
            }
            else
            {
                _rb.velocity = Vector2.up * _jumpForce;
            }

            _hangTimeCounter = 0f;
            _jumpBufferCounter = 0f;
        }
    }

    private void HandleHoldJump()
    {
        if (_rb.velocity.y < 0f)
        {
            _rb.gravityScale = _fallMultiplier;
        }

        if (_inputHandler.IsJumpPressed())
        {
            _rb.gravityScale = _holdJumpMultiplier;
        }
        else if (_rb.velocity.y > 0f)
        {
            _rb.gravityScale = _tapJumpMultiplier;
        }
    }

    private void HandleHangTime()
    {
        _hangTimeCounter = _playerGroundCheck.GroundedCheck() ? _hangTime : Mathf.Max(0f, _hangTimeCounter - Time.deltaTime);
    }
}
