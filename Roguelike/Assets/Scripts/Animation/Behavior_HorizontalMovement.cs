using UnityEngine;

public class Behavior_HorizontalMovement : MonoBehaviour
{
    private AnimationController _states;
    private Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed = 5f;

    private void Awake()
    {
        _states = GetComponent<AnimationController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_states.WalkingState)
        {
            Move();
        }
        else
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        _rb.velocity = Vector3.zero;
    }

    private void Move()
    {
        _rb.velocity = Vector2.right * _moveSpeed;
    }
}
