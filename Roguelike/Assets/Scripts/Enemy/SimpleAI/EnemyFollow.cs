using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;

    public float minimunDistance = 10f;
    public float moveSpeed = 2f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            // attack
        }
    }

    private void Chase()
    {
        
        //transform.LookAt(target.position);

        // Calculate the direction to the player
        Vector2 direction = (target.position - transform.position).normalized;

        // Calculate the angle between the current position and the player
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the enemy to face the player
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        

        // Flip the sprite horizontally depending on the player's position
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

}
