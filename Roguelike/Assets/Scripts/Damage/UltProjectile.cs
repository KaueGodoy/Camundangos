using UnityEngine;

public class UltProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private AudioManager _audioManager;

    [Header("Damage")]
    public float projectileDamage = 6f;

    [Header("Distance")]
    public float projectileDistance = 2f;

    [Header("Speed/Angle")]
    public float projectileSpeed = 5f;
    public float angle = 45f;

    private float _radians;
    private float _horizontalVelocity;
    private float _verticalVelocity;
    private bool _isFacingRight;

    private void Awake()
    {
        _isFacingRight = FindObjectOfType<PlayerController>().IsFacingRight;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _radians = angle * Mathf.Deg2Rad;

        if (_isFacingRight)
        {
            _horizontalVelocity = projectileSpeed * Mathf.Cos(_radians);
            _verticalVelocity = -projectileSpeed * Mathf.Sin(_radians);
            _rb.velocity = new Vector2(_horizontalVelocity, _verticalVelocity);

        }
        else
        {
            _spriteRenderer.flipX = false;
            _horizontalVelocity = -projectileSpeed * Mathf.Cos(_radians);
            _verticalVelocity = -projectileSpeed * Mathf.Sin(_radians);
            _rb.velocity = new Vector2(_horizontalVelocity, _verticalVelocity);
        }

    }

    private void Update()
    {
        Destroy(this.gameObject, projectileDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<IEnemy>().TakeDamage(projectileDamage);
            DamagePopup.Create(transform.position, (int)projectileDamage);
            _audioManager.PlaySound("Hitmarker");
            //Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
