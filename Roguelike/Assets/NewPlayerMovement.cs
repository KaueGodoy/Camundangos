using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerJump += GameInput_OnPlayerJump;
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
        _rb.velocity = Vector2.up * _jumpForce;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * _moveSpeed;
    }

}
