using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolJump : MonoBehaviour
{
    public Player player;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Attack")]
    public float attackRange = 5f;
    public float damage = 300f;

    [Header("Jump")]
    public float jumpForce = 15f;
    public float jumpTime = 0.3f;
    public float jumpCooldown = 1.5f;
    public float lowJumpMultiplier = 1f;
    public float fallMultiplier = 2.5f;
    public float lastJumpTime;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    private bool isCritical;

    private FishState currentState;

    public enum FishState
    {
        Patrol, Jump
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentState = FishState.Patrol;
    }

    private void Update()
    {
        FlipSprite();

    }

    private void FixedUpdate()
    {
        EnemyBehavior(currentState);
        Debug.Log("State: " + currentState);
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
        }

        if (Time.time - lastJumpTime > jumpCooldown)
        {
            currentState = FishState.Jump;
            lastJumpTime = Time.time;
        }

        CancelInvoke("PerformAttack");
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));

        lastJumpTime = Time.time;

        StartCoroutine(WaitForPatrol());

    }
    IEnumerator WaitForPatrol()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        currentState = FishState.Patrol; // Switch back to Patrol state
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            PerformAttack();
        }
    }

    private void Idle()
    {
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
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
