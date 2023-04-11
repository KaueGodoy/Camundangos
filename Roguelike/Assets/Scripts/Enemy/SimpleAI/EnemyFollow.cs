using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;

    [Header("Attack")]
    public Player player;
    public float attackRange = 5f;
    public float damage = 300f;

    public Transform target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < attackRange)
        {
            isChasing = false;
            Attack();

        }
        else if (distance < chaseRange)
        {
            isChasing = true;
            Chase();
        }
        else
        {
            isChasing = false;
            Idle();
        }

    }

    private void Chase()
    {
        direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        CancelInvoke("PerformAttack");
        Flip();
        //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    }

    private void Flip()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Attack()
    {
        if (!IsInvoking("PerformAttack"))
        {
            InvokeRepeating("PerformAttack", .5f, 1.5f);
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(damage);
    }

    private void Idle()
    {
        // do nothing
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
        // Debug.Log("In idle state...");
    }

  



    //transform.LookAt(target.position);
    // Calculate the angle between the current position and the player
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Set the rotation of the enemy to face the player
    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}
