using UnityEngine;

public abstract class DamageableWithHealthBar : Damageable
{
    private bool _isHit = false;
    public bool IsHit { get { return _isHit; } set { _isHit = value; } }

    public override void TakeDamage(float amount)
    {
        if (CurrentHealth == MaxHealth)
        {
            InstantiateHealthBar();
        }

        IsHit = true;
        CurrentHealth -= amount;
        _healthSystem.Damage(amount);
        DamagePopup.Create(transform.position, (int)amount);

        if (CurrentHealth <= HealthThreshold)
        {
            DisableHealthBar();
        }
    }

    private HealthSystem _healthSystem;
    private EnemyHealthBar _healthBar;
    private Transform _healthBarTransform;

    [Header("HP Bar")]
    [SerializeField] private Transform _healthBarPrefab;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1f);

    private void InstantiateHealthBar()
    {
        _healthSystem = new HealthSystem(MaxHealth);
        _healthBarTransform = Instantiate(_healthBarPrefab, transform.position + _offset, Quaternion.identity, transform);
        _healthBar = _healthBarTransform.GetComponent<EnemyHealthBar>();
        _healthBar.Setup(_healthSystem);

        Debug.Log("Health: " + _healthSystem.GetHealthPercent());
        Debug.Log("Health: " + _healthSystem.GetCurrentHealth());
    }

    private void DisableHealthBar()
    {
        _healthSystem.Die();
        _healthBarTransform.gameObject.SetActive(false);
    }

    [Header("Timers")]
    private float _hitCooldown = 0.3f;
    private float _hitTimer = 0.0f;

    private void UpdateTimers()
    {
        if (IsHit)
        {
            _hitTimer += Time.deltaTime;
        }

        if (_hitTimer > _hitCooldown)
        {
            IsHit = false;
            _hitTimer = 0f;
        }
    }

    private void Update()
    {
        UpdateTimers();
    }
}
