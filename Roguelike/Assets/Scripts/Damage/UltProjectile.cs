using UnityEngine;

public class UltProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Damage")]
    public float projectileDamage = 6f;

    [Header("Speed/Angle")]
    public float projectileSpeed = 5f;
    public float angle = 45f;

    private float radians;
    private float xVelocity;
    private float yVelocity;

    [Header("Distance")]
    public float projectileDistance = 2f;

    public CharacterStats characterStats { get; set; }

    private bool _isFacingRight;

    private void Awake()
    {
        _isFacingRight = FindObjectOfType<PlayerController>().isFacingRight;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //rb.velocity = transform.right * projectileSpeed;
        //rb.velocity = new Vector2(projectileSpeed, -projectileSpeed);
        radians = angle * Mathf.Deg2Rad;

        if (_isFacingRight)
        {
            //rb.velocity = new Vector2(projectileSpeed, -projectileSpeed);     
            xVelocity = projectileSpeed * Mathf.Cos(radians);
            yVelocity = -projectileSpeed * Mathf.Sin(radians);
            rb.velocity = new Vector2(xVelocity, yVelocity);

        }
        else
        {
            // rb.velocity = new Vector2(-projectileSpeed, -projectileSpeed);
            _spriteRenderer.flipX = false;
            xVelocity = -projectileSpeed * Mathf.Cos(radians);
            yVelocity = -projectileSpeed * Mathf.Sin(radians);
            rb.velocity = new Vector2(xVelocity, yVelocity);
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
            FindObjectOfType<AudioManager>().PlaySound("Hitmarker");
            //Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
