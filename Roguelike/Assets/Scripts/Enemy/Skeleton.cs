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

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Damage")]
    public float damage = 5f;
    public float attackRange = 2f;

    HealthSystem healthSystem;
    Transform healthBarTransform;

    public int ID { get; set; }

    void Start()
    {
        currentHealth = maxHealth;

        healthSystem = new HealthSystem(maxHealth);

        healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, transform.rotation, transform);
        healthBarTransform.gameObject.SetActive(false);

        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        Debug.Log("Health: " + healthSystem.GetCurrentHealth());


    }
    public void PerformAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSystem.Damage(damage);
        healthBarTransform.gameObject.SetActive(true);

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
