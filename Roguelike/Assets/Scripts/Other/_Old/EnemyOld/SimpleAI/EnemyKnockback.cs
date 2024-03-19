using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;

    [SerializeField] private bool _isKnockbackActive = false;
    [SerializeField] private Vector2 _knockbackDirection;
    [SerializeField] private float _knockbackTimer;

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
            // Handle knockback recovery here (e.g., re-enable enemy behavior)
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

            // Start knockback effect
            _isKnockbackActive = true;
            _knockbackTimer = knockbackDuration;

            // Disable player input or apply restrictions if needed
        }
    }
}
