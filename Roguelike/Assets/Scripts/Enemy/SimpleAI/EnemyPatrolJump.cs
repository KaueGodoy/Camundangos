using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolJump : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Attack")]
    public Player player;
    public float attackRange = 5f;
    public float damage = 300f;

    [Header("Jumo")]
    public float jumpForce = 15f;


    public Transform target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    private bool isCritical;

    public enum FishState
    {
        Patrol, Jump
    }

    private FishState currentState = FishState.Patrol;


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

        if (Vector2.Distance(transform.position, target.position) < 5f)
        {
            currentState = FishState.Jump;
        }
        else
        {
            currentState = FishState.Patrol;
        }

        //Patrol();

    }

    private void FixedUpdate()
    {
        EnemyBehavior(currentState);
    }

    private void EnemyBehavior(FishState currentState)
    {
        switch (currentState)
        {
            case FishState.Patrol:
                Patrol();
                break;
            case FishState.Jump:
                Jump();
                break;
        }
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
            Debug.Log(patrolIndex);
        }

        CancelInvoke("PerformAttack");
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
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
        DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage, isCritical);

    }

    private void Idle()
    {
        // do nothing
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
        // Debug.Log("In idle state...");
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
