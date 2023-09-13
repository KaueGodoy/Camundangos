using UnityEngine;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Enemy dead: " + gameObject.name);
            }
        }
    }

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }
}
