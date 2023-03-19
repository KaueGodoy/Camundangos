using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{

    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 200;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Damage")]
    public float damage = 5f;
    public float attackRange = 2f;

    [Header("Aggro")]
    public float aggroRange = 20f;
    public Player player;
    public LayerMask aggroLayerMask;

    private CharacterStats characterStats;
    private Collider[] withinAggroColliders;

    public int ID { get; set; }

    void Start()
    {
        characterStats = new CharacterStats(10, 5, 2);
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < attackRange)
        {
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 1.5f);
            }
        }
        else if (distance < aggroRange)
        {
            ChasePlayer();
            CancelInvoke("PerformAttack");
            //Debug.Log("Player found! " + distance);
        }


        /* 3D only?
        withinAggroColliders = Physics.OverlapSphere(transform.position, 100, aggroLayerMask);
        if (withinAggroColliders.Length > 0)
        {
            Debug.Log("Player found!");
        }*/
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
    }

    public void PerformAttack()
    {
        player.TakeDamage(damage);
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
