using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Attack")]
    public Player player;
    public float attackRange = 5f;
    public float damage = 300f;

    public Transform target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    private bool isCritical;

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
            isPatrolling = false;
            Attack();

        }
        else if (distance < chaseRange)
        {
            isChasing = true;
            isPatrolling = false;
            Chase();
        }
        else
        {
            isChasing = false;
            isPatrolling = true;
            Patrol();
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
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            // flipping the player using scale
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return; // no patrol points defined

        direction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            // switch to the next patrol point
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
           
        }

        Flip();
        CancelInvoke("PerformAttack");
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
        DamagePopup.Create(player.transform.position, (int)damage, isCritical);
    }

    private void Idle()
    {
        // do nothing
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
        // Debug.Log("In idle state...");
    }

    private void Flip2()
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


    //transform.LookAt(target.position);
    // Calculate the angle between the current position and the player
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Set the rotation of the enemy to face the player
    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}
