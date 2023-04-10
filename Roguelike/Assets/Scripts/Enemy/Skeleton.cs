using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemy
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 200;

    [Header("HP Bar")]
    public Transform pfHealthBar;
    public Vector3 offset = new Vector3(0, 1f);

    HealthSystem healthSystem;
    Transform healthBarTransform;

    public int ID { get; set; }

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void PerformAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        if (currentHealth == maxHealth)
        {
            InstantiateHealthBar();
        }

        currentHealth -= damage;
        healthSystem.Damage(damage);

        //Debug.Log("Health: " + healthSystem.GetHealthPercent());
        //Debug.Log("Health: " + healthSystem.GetCurrentHealth());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void InstantiateHealthBar()
    {
        healthSystem = new HealthSystem(maxHealth);

        healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, transform.rotation, transform);
        //healthBarTransform.gameObject.SetActive(false);

        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        Debug.Log("Health: " + healthSystem.GetCurrentHealth());
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
