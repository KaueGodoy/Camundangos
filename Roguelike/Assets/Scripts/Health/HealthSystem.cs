using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    public event EventHandler OnDamaged;

    private float _currentHealth;
    private float _maxHealth;

    public HealthSystem(float maxHealth)
    {
        this._maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetHealthPercent()
    {
        return _currentHealth / _maxHealth;
    }

    public void Damage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }

}
