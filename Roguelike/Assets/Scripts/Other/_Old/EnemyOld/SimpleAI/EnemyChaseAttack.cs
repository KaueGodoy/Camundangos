using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseAttack : MonoBehaviour
{
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
            // Get the Player component from the player object
            player = playerObject.GetComponent<PlayerController>();
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
        UpdateCooldowns();

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

    private void UpdateCooldowns()
    {
        // attack
        if (attackAnimation)
        {
            attackTimer += Time.deltaTime;
        }
        if (attackTimer > attackDelay)
        {
            attackAnimation = false;
            attackTimer = 0f;
        }
    }

    #region Movement

    /// <summary>
    /// move the character left and right
    /// chase the player 
    /// flip the sprite
    /// </summary>

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = direction * moveSpeed;
        }

        rb.mass = 10f;

        if (rb.velocity.y <= 0.1f)
        {
            rb.gravityScale = 50f;
        }
        else
        {
            rb.gravityScale = 1f;
        }

    }

    private void Chase()
    {
        isChasing = true;
        CancelInvoke();
        Move();
    }

    private void FlipSprite()
    {
        if (direction.x > 0f && isFacingRight || direction.x < 0f && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    #endregion


    #region Attack

    /// <summary>
    /// attack the player when within range
    /// </summary>

    [Header("Attack")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float damage = 300f;

    [HideInInspector] public bool attackAnimation = false;

    private float attackTimer = 0.0f;
    private float attackDelay = 0.4f;

    private PlayerController player;

    private void Attack()
    {
        isChasing = false;
        direction = Vector2.zero;
        attackAnimation = true;

        if (!IsInvoking("PerformAttack"))
        {
            InvokeRepeating("PerformAttack", .5f, .5f);
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(damage);
        DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage);
    }

    #endregion

    /// <summary>
    /// do nothing
    /// play idle animation
    /// </summary>

    private void Idle()
    {
        isChasing = false;
        direction = Vector2.zero;
        CancelInvoke();
    }
}
