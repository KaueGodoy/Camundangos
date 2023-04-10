using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;

    [Header("Attack")]
    public Player player;
    public float attackRange = 5f;
    public float damage = 300f;

    public Transform target;

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
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < chaseRange)
        {
            Chase();

            if (distance < attackRange)
            {

                if (!IsInvoking("PerformAttack"))
                {
                    InvokeRepeating("PerformAttack", .5f, 1.5f);
                }

            }
            else
            {
                CancelInvoke("PerformAttack");
            }
        }
        else
        {
            direction = Vector2.zero;
            CancelInvoke("PerformAttack");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }

    private void Chase()
    {
        direction = (target.position - transform.position).normalized;
        Flip();
        //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    }

    private void Flip()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(damage);
    }



    //transform.LookAt(target.position);
    // Calculate the angle between the current position and the player
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Set the rotation of the enemy to face the player
    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}
