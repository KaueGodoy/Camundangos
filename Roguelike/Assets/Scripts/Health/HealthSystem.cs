using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    public event EventHandler OnDamaged;

    private float currentHealth;
    private float maxHealth;

    public HealthSystem(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public void Damage(float damageAmount)
    {
        OnDamaged?.Invoke(this, EventArgs.Empty);

        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }

}
