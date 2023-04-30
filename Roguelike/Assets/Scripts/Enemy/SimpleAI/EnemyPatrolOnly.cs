using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyPatrolJump;

public class EnemyPatrolOnly : MonoBehaviour
{
    public Player player;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Attack")]
    public float attackRange = 5f;
    public float damage = 300f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

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
        FlipSprite();
        Patrol();
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return; // no patrol points defined

        direction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        Move();

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            // switch to the next patrol point
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        CancelInvoke("PerformAttack");
    }

    private void Move()
    {
        rb.velocity = direction * moveSpeed;
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
        DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            player.TakeDamage(damage);
            DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage);
        }
    }

    private void FlipSprite()
    {
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = isFacingRight;
        }

    }
}
