using System.Collections;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;


    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    private Coroutine _dashCoroutine;

    [Header("Ground")]
    [SerializeField] private LayerMask _jumpableGround;

    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rb;

    public float CurrentRotation { get { return transform.rotation.y >= 0 ? 1 : -1; } set { } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        Move();
    }

    public void Move()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector2 moveDir = new Vector2(inputVector.x * _moveSpeed, _rb.velocity.y);

        if (moveDir != Vector2.zero)
        {
            _rb.velocity = moveDir;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        if (!IsGrounded()) return;

        _rb.velocity = Vector2.up * _jumpForce;
    }

    private void Dash()
    {
        CallDashCoroutine();
    }

    private IEnumerator DashCoroutine()
    {
        // audio

        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        _rb.velocity = new Vector2(CurrentRotation * _dashSpeed, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        tr.emitting = false;
        _rb.gravityScale = originalGravity;

        yield return new WaitForSeconds(_dashingCooldown);
    }

    private void CallDashCoroutine()
    {
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    private bool IsGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center,
                                                    _boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down,
                                                    extraHeightText, _jumpableGround);

        return raycastHit.collider != null;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * _moveSpeed;
    }

}
