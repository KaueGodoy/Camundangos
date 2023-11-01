using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementNew : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveH = _inputHandler.GetMovementInput();

        Vector2 direction = new Vector2(moveH.x * _moveSpeed, _rb.velocity.y);

        if (direction != Vector2.zero)
        {
            _rb.velocity = direction;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

}
