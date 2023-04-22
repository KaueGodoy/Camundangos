using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseAttack : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;

    [Header("Attack")]
    public Player player;
    public float attackRange = 5f;
    public float damage = 300f;

    public Transform target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    private bool isCritical;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        direction = (target.position - transform.position).normalized;
        FlipSprite();

        if (distance < attackRange)
        {
            Attack();

        }
        else if (distance < chaseRange)
        {
            Chase();
        }
        else
        {
            Idle();
        }

    }

    private void Chase()
    {
        isChasing = true;
        Move();

        if (IsInvoking("PerformAttack"))
        {
            CancelInvoke("PerformAttack");
        }
    }

    private void Move()
    {
        rb.velocity = direction * moveSpeed;
    }

    private void Attack()
    {
        isChasing = false;
        direction = Vector2.zero;

        if (!IsInvoking("PerformAttack"))
        {
            InvokeRepeating("PerformAttack", .5f, .5f);
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(damage);
        DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage, isCritical);

    }

    private void Idle()
    {
        // do nothing
        isChasing = false;
        direction = Vector2.zero;

        if (IsInvoking("PerformAttack"))
        {
            CancelInvoke("PerformAttack");
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
