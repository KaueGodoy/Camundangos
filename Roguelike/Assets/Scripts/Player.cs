using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // components
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Health")]
    public float currentHealth = 0;
    public float maxHealth = 3;
    private bool isAlive;
    private readonly float deathAnimationTime = 0.8f;

    [Space]
    public float hitCooldown = 0.3f;
    private float hitTimer = 0.0f;
    private bool isHit = false;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private float moveX;

    private bool isFacingRight = true;

    [Header("Jump")]
    public float jumpForce = 5f;
    private bool jumpRequest = false;

    [Header("Attack")]
    public float attackDelay = 0.4f;
    private float attackTimer = 0.0f;

    private bool attackRequest = false;
    private bool attackAnimation = false;
    private bool isAttacking = false;

    [Header("Ground")]
    [SerializeField] private LayerMask jumpableGround;

    // animation
    private Animator animator;
    private string currentAnimation;

    private const string PLAYER_DEATH = "derildo_death";
    private const string PLAYER_HIT = "derildo_hit";
    private const string PLAYER_ATTACK = "derildo_attack";
    private const string PLAYER_JUMP = "derildo_jump";
    private const string PLAYER_FALL = "derildo_fall";
    private const string PLAYER_WALK = "derildo_walk";
    private const string PLAYER_IDLE = "derildo_idle";


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        
    }
    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }
    void Update()
    {
        if (isAlive)
        {
            ProcessInput();
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
            Jump();
            Flip();
        }

        UpdateTimers();
        UpdateAnimationState();
    }

    private void ProcessInput()
    {
        // horizontal movement
        moveX = Input.GetAxisRaw("Horizontal");

        // jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequest = true;
        }

        // attack
        if (Input.GetButtonDown("Fire1"))
        {
            attackRequest = true;
        }

        // damage test // DELETE
        if (Input.GetButtonDown("Fire2"))
        {
            TakeDamage(1);
        }

    }

    #region timers
    private void UpdateTimers()
    {
        // hit
        if (isHit)
        {
            hitTimer += Time.deltaTime;
        }

        if (hitTimer > hitCooldown)
        {
            isHit = false;
            hitTimer = 0f;
        }

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
    #endregion timers

    #region movement
    private void Move()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (jumpRequest)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpRequest = false;
        }
    }
    #endregion movement

    #region attack
    private void Attack()
    {
        if (attackRequest)
        {
            attackRequest = false;
            attackAnimation = true;

            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("Attacking");

                Invoke("AttackComplete", attackDelay);
            }
        }
    }

    private void AttackComplete()
    {
        isAttacking = false;
    }
    #endregion attack

    private void Flip()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            // flipping the player using scale

            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
            //.Rotate(firePoint.rotation.x, 180f, firePoint.rotation.z);

        }

    }

    private void RestartLevel()
    {
        isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Die()
    {
        isAlive = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void TakeDamage(float damageAmount)
    {
        currentHealth -= Mathf.FloorToInt(damageAmount);
        isHit = true;

        if (currentHealth <= 0)
        {
            Die();

            Invoke("RestartLevel", deathAnimationTime);
        }
    }


    #region animation

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;

    }

    private void UpdateAnimationState()
    {
        // death
        if (!isAlive)
        {
            ChangeAnimationState(PLAYER_DEATH);
        }
        // hit
        else if (isHit)
        {
            ChangeAnimationState(PLAYER_HIT);
        }
        // attack
        else if (attackAnimation)
        {
            ChangeAnimationState(PLAYER_ATTACK);
        }
        // jump
        else if (rb.velocity.y > .1f && !IsGrounded())
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
        // fall
        else if (rb.velocity.y < .1f && !IsGrounded())
        {
            ChangeAnimationState(PLAYER_FALL);
        }
        // move
        else if (moveX > 0 || moveX < 0)
        {
            ChangeAnimationState(PLAYER_WALK);
        }
        // idle
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
    }
    #endregion animation

    private bool IsGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, extraHeightText, jumpableGround);

        // draw gizmos
        /*

        Color rayColor;

        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);
        Debug.Log(raycastHit.collider);

         */

        return raycastHit.collider != null;

    }

}
