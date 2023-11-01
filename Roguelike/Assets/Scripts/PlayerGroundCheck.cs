using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerGroundCheck : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _jumpableGround;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool GroundedCheck()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(
            _boxCollider.bounds.center,
            _boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f),
            0f,
            Vector2.down,
            extraHeightText,
            _jumpableGround
        );

        return IsGrounded = raycastHit.collider != null;
    }
}
