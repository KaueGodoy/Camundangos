using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Range { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }

    private Vector2 _spawnPosition;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _spawnPosition = transform.position;
        Range = 10f;
        Speed = 70f;
        Damage = 250f;
        GetComponent<Rigidbody2D>().AddForce(Direction * Speed);
    }

    private void Update()
    {
        if (Vector2.Distance(_spawnPosition, transform.position) >= Range)
            Extinguish();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(Damage);
            _audioManager.PlaySound("Hitmarker");
            Debug.Log($"Dealing {Damage} damage to {collision.name}");
            Extinguish();
        }
    }

    private void Extinguish()
    {
        Destroy(gameObject);
    }

}
