using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    public float currentHealth, power, defense;
    public float maxHealth = 10;

    private CharacterStats characterStats;

    public int ID { get; set; }

    void Start()
    {
        characterStats = new CharacterStats(10, 5, 2);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


}
