using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private bool isKnockbackActive = false;
    private Vector2 knockbackDirection;
    private float knockbackTimer;

    private void Update()
    {
        if (isKnockbackActive)
        {
            ApplyKnockback();
        }
    }

    private void ApplyKnockback()
    {
        knockbackTimer -= Time.deltaTime;

        if (knockbackTimer <= 0f)
        {
            isKnockbackActive = false;
            // Handle knockback recovery here (e.g., re-enable enemy behavior)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isKnockbackActive && collision.gameObject.CompareTag("Player"))
        {
            // Calculate knockback direction
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = collision.gameObject.transform.position;
            knockbackDirection = (playerPosition - enemyPosition).normalized;

            // Apply knockback force to the player
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = knockbackDirection * knockbackForce;

            // Start knockback effect
            isKnockbackActive = true;
            knockbackTimer = knockbackDuration;

            // Disable player input or apply restrictions if needed
        }
    }
}
