using UnityEngine;

public class PlayerSkill_Projectile : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _travelSpeed = 5f;
    [SerializeField] private float _travelDistance = 2f;

    private float _damage = 6f;
    private bool _isCritical;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _damage = PlayerSkill.Instance.FinalDamage;

        _rb.velocity = transform.right * _travelSpeed;
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
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
