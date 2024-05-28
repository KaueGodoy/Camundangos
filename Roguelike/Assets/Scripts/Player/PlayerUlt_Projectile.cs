using UnityEngine;

public class PlayerUlt_Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    [Header("Stats")]
    [SerializeField] private float _damage = 6f;
    [SerializeField] private float _travelSpeed = 5f;
    [SerializeField] private float _travelDistance = 2f;
    [SerializeField] private float _angle = 45f;

    private float _radians;
    private float _horizontalVelocity;
    private float _verticalVelocity;
    private bool _isFacingRight;

    private void Awake()
    {
        _isFacingRight = NewPlayerMovement.Instance.IsFacingRight;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _radians = _angle * Mathf.Deg2Rad;

        if (_isFacingRight)
        {
            _horizontalVelocity = _travelSpeed * Mathf.Cos(_radians);
            _verticalVelocity = -_travelSpeed * Mathf.Sin(_radians);
            _rb.velocity = new Vector2(_horizontalVelocity, _verticalVelocity);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _horizontalVelocity = -_travelSpeed * Mathf.Cos(_radians);
            _verticalVelocity = -_travelSpeed * Mathf.Sin(_radians);
            _rb.velocity = new Vector2(_horizontalVelocity, _verticalVelocity);
        }

    }

    private void Update()
    {
        Destroy(this.gameObject, _travelDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Dealing damage: " + _damage);

            AudioManager.Instance.PlaySound("Hitmarker");
            //Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
