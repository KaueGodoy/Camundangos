using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header("Stats")]
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _projectileDamage = 300f;
    [SerializeField] private float _projectileTimeToDestroy = 4f;

    private Transform _playerTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Start()
    {
        Vector3 direction = _playerTransform.transform.position - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * _projectileSpeed;

        float bulletRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bulletRotation + 90f);
    }

    private void Update()
    {
        Destroy(gameObject, _projectileTimeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(_projectileDamage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
