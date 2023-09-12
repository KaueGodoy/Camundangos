using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioManager _audioManager;

    [Header("Damage")]
    public float projectileDamage = 6f;

    [Header("Burning")]
    public float burnAmount = 4f;

    [Header("Speed")]
    public float projectileSpeed = 5f;

    [Header("Distance")]
    public float projectileDistance = 2f;
    public bool isCritical;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _rb.velocity = transform.right * projectileSpeed;
    }

    private void Update()
    {
        Destroy(this.gameObject, projectileDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check for collision
        //StatusEffectManager enemy = collision.GetComponent<StatusEffectManager>();
        //if (enemy != null)
        //{
        //    enemy.ApplyProjectileDamage(projectileDamage);
        //    enemy.ApplyBurn(burnAmount);
        //}

        // check for enemy HP
        //Health health = collision.GetComponent<Health>();
        //if (health != null)
        //{
        //    if (health.currentHealth <= 0)
        //    {
        //        Destroy(collision.gameObject);
        //    }
        //}

        // check collision with player - not destryoing the bullet
        //PlayerController player = collision.GetComponent<PlayerController>();
        //if (!player)
        //{
        //    Destroy(gameObject);
        //}

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<IEnemy>().TakeDamage(projectileDamage);
            DamagePopup.Create(transform.position, (int)projectileDamage, isCritical);
            _audioManager.PlaySound("Hitmarker");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
