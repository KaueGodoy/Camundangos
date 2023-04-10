using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float minimunDistance = 10f;
    public float moveSpeed = 2f;

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

        if (distance < minimunDistance)
        {
            Chase();
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }

    private void Chase()
    {

        direction = (target.position - transform.position).normalized;

        // flip
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    }




    //transform.LookAt(target.position);
    // Calculate the angle between the current position and the player
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Set the rotation of the enemy to face the player
    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}
