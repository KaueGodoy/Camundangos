using UnityEngine;

public abstract class DamageableWithHealthBar : Damageable
{
    public override void TakeDamage(float amount)
    {
        if (CurrentHealth == MaxHealth)
        {
            InstantiateHealthBar();
        }

        CurrentHealth -= amount;
        _healthSystem.Damage(amount);
        DamagePopup.Create(transform.position, (int)amount);

        if (CurrentHealth <= 0)
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

}
