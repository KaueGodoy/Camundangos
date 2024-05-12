using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;

    [SerializeField] private bool _isKnockbackActive = false;
    [SerializeField] private Vector2 _knockbackDirection;
    [SerializeField] private float _knockbackTimer;

    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _minRandomMultiplier = 1f;
    [SerializeField] private float _maxRandomMultiplier = 2f;

    public float Damage
    {
        get
        {
            float randomMultiplier = UnityEngine.Random.Range(_minRandomMultiplier, _maxRandomMultiplier);

            return _damage * randomMultiplier;
        }

        set { _damage = value; }
    }

    private void Update()
    {
        if (_isKnockbackActive)
        {
            ApplyKnockback();
        }
    }

    private void ApplyKnockback()
    {
        _knockbackTimer -= Time.deltaTime;

        if (_knockbackTimer <= 0f)
        {
            _isKnockbackActive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isKnockbackActive && collision.gameObject.CompareTag("Player"))
        {
            // Calculate knockback direction
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = collision.gameObject.transform.position;
            _knockbackDirection = (playerPosition - enemyPosition).normalized;

            // Apply knockback force to the player
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = _knockbackDirection * knockbackForce;

            _isKnockbackActive = true;
            _knockbackTimer = knockbackDuration;

            NewPlayerController.Instance.TakeDamage(Damage);

        }

    }
}
