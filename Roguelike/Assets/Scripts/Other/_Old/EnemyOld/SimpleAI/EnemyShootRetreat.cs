using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShootRetreat : MonoBehaviour
{
    private NewPlayerController player;
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 direction;

    [SerializeField] private Transform _projectileOrigin;

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
            player = playerObject.GetComponent<NewPlayerController>();
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

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void Update()
    {
        UpdateCooldowns();

        float distance = Vector2.Distance(transform.position, target.position);
        direction = (target.position - transform.position).normalized;
        FlipSprite();

        if (distance < retreatRange)
        {
            isIdle = false;
            isChasing = false;
            Retreat();
        }
        else if (distance < shootRange)
        {
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
            //direction = Vector2.zero;
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
    /// evades the player to the opposite direction
    /// </summary>

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;
    public bool isIdle;

    private void Chase()
    {
        Move();
        CancelInvoke();
    }

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = direction * moveSpeed;
        }
    }

    private void FlipSprite()
    {
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            //spriteRenderer.flipX = isFacingRight;


            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Retreat()
    {
        // rb add force
        Vector2 retreatDirection = (transform.position - target.position).normalized;
        rb.AddForce(retreatDirection * retreatForce, ForceMode2D.Impulse);
    }

    //private void Retreat2()
    //{
    //    // transform
    //    rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, (-moveSpeed * retreatSpeed) * Time.deltaTime));
    //}

    //private void Retreat3()
    //{
    //    // rb only x axis
    //    float newX = Mathf.MoveTowards(transform.position.x, target.position.x, -moveSpeed * retreatSpeed * Time.deltaTime);
    //    Vector2 newPosition = new Vector2(newX, transform.position.y);
    //    rb.MovePosition(newPosition);
    //}

    #endregion

    #region Attack
    /// <summary>
    /// shoot projectile at player
    /// </summary>

    [Header("Range")]
    public float shootRange = 10f;
    public float retreatRange = 2.5f;
    public float retreatSpeed = 5f;
    public float retreatForce = 0.4f;

    [Header("Projectile")]
    [SerializeField] private float attackDuration = 0.6f;
    [SerializeField] private float shotRate = 0.6f;
    public GameObject projectile;

    // Timers
    [HideInInspector] public bool attackAnimation = false;

    private float attackTimer = 0.0f;
    private float attackDelay = 0.85f;

    private void Attack()
    {
        isChasing = false;
        direction = Vector2.zero;
        attackAnimation = true;

        if (!IsInvoking("InstatiateShot"))
        {
            InvokeRepeating("InstatiateShot", attackDuration, shotRate);
        }
    }

    private void InstatiateShot()
    {
        Instantiate(projectile, _projectileOrigin.position, Quaternion.identity);
    }
    #endregion
}
