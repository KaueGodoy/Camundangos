using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShootRetreat : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;
    public bool isIdle;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    [Header("Target")]
    public Transform target;

    [Header("Range")]
    public float shootRange = 10f;
    public float retreatRange = 2.5f;
    public float retreatSpeed = 5f;


    [Header("Projectile")]
    public float timeBetweenShots = 1.5f;
    public float nextShotTime;
    public GameObject projectile;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < retreatRange)
        {
            isIdle = false;
            isChasing = false;
            Retreat();
        }
        else if (distance < shootRange)
        {
            direction = Vector2.zero;
            isIdle = false;
            isChasing = false;
            Attack();
        }
        else if (distance < chaseRange)
        {
            isChasing = true;
            isIdle = false;
            Chase();
        }
        else
        {
            // idle
            isChasing = false;
            isIdle = true;
            direction = Vector2.zero;
        }
    }

    private void Chase()
    {
        direction = (target.position - transform.position).normalized;
        Move();
        CancelInvoke("InstatiateShot");
        FlipSprite();
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

    private void Retreat()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, (-moveSpeed * retreatSpeed) * Time.deltaTime));
    }

    private void Attack()
    {
        if (!IsInvoking("InstatiateShot"))
        {
            InvokeRepeating("InstatiateShot", .5f, 1.5f);
        }
    }
    private void InstatiateShot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

}
