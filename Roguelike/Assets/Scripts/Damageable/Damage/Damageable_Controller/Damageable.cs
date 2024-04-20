using UnityEngine;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    private readonly float _healthThreshold = 0.0f;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            if (_currentHealth <= HealthThreshold)
            {
                Die();
            }
        }
    }

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float HealthThreshold { get { return _healthThreshold; } private set { } }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        DamagePopup.Create(transform.position, (int)amount);
    }

    [Header("Death")]
    [SerializeField] private float _deathAnimationTime = 0.55f;
    [SerializeField] private GameObject _deathEffect;

    private void DeathEffect()
    {
        if (_deathEffect != null)
        {
            this.gameObject.SetActive(false);
            GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
        }
    }

    public virtual void Die()
    {
        Invoke("DeathEffect", _deathAnimationTime);

        Destroy(gameObject, _deathAnimationTime);
        Debug.Log("Enemy dead: " + gameObject.name);
    }
}
