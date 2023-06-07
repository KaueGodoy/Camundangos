using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolChaseAttack : MonoBehaviour
{
    private Player player;
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            target = playerObject.transform;

            if (player == null)
            {
                Debug.LogError("Player component not found on the player object!");
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        direction = (target.position - transform.position).normalized;

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

        FlipSprite();
    }

    #region Movement

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return; 

        direction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        Move();

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

    }

    private void Chase()
    {
        Move();
        CancelInvoke();
    }

    private void Move()
    {
        rb.velocity = direction * moveSpeed;
    }

    private void FlipSprite()
    {
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = isFacingRight;
        }
    }

    private void Idle()
    {
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
    }

    #endregion


    #region Attack

    [Header("Attack")]
    public float attackRange = 5f;
    public float damage = 300f;

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

    #endregion
}
