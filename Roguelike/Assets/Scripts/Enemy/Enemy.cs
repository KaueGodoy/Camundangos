using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // components
    public GameObject player;
    private Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 10;
    private float moveX;

    [Header("Health")]
    public float currentHealth = 0;
    public float maxHealth = 3;
    private readonly float deathAnimationTime = 0.8f;

    private bool isAlive;
    private float hitTime = 0.3f;
    private bool isHit = false;

    [Header("Range")]
    public float rangeDistance = 10f;

    /*
    [Header("Waypoints")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    private SpriteRenderer spriteRenderer;*/

    // animation
    private Animator animator;
    private string currentAnimation;

    private const string ENEMY_DEATH = "skeleton_death";
    private const string ENEMY_HIT = "skeleton_hit";
    private const string ENEMY_ATTACK = "skeleton_attack";
    private const string ENEMY_WALK = "skeleton_walk";
    private const string ENEMY_IDLE = "skeleton_idle";



    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.flipX = false;

        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
        isAlive = true;
    }


    private void FixedUpdate()
    {
        
        UpdateAnimationState();
        
    }
    // delete
    #region waypoints
    /*
    private void WaypointMovement()
    {
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex > waypoints.Length)
            {
                currentWaypointIndex = 0;
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }


    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

    */
    #endregion waypoints

    #region animation
    private void UpdateAnimationState()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // death
        if (!isAlive)
        {
            ChangeAnimationState(ENEMY_DEATH);
        }
        // hit
        else if (isHit)
        {
            ChangeAnimationState(ENEMY_HIT);
        }
        // attack
        else if (distance < rangeDistance)
        {
            ChangeAnimationState(ENEMY_ATTACK);
        }
        // roam
        else if (moveX > 0 || moveX < 0)
        {
            ChangeAnimationState(ENEMY_WALK);
        }
        // idle
        else
        {
            ChangeAnimationState(ENEMY_IDLE);
        }
    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;

    }
    #endregion animation

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            FindObjectOfType<AudioManager>().PlaySound("Hitmarker");
            TakeDamage(1);


            if (currentHealth <= 0)
            {
                IsDead();

            }

        }
    }

    private void TakeDamage(float damage)
    {
        isHit = true;
        currentHealth -= damage;

        Invoke("HitComplete", hitTime);
    }

    private void IsDead()
    {
        isAlive = false;
        Invoke("Die", deathAnimationTime);
    }

    private void HitComplete()
    {
        isHit = false;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
