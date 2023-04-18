using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    public float moveSpeed = 5f;
    public float timeToDestroy = 4f;
    public float damage = 300f;
    private bool isCritical;

    Rigidbody2D rb;

    private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //targetPosition = FindObjectOfType<Player>().transform.position;

        // calculates the player direction
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;

        // rotates the bullet
        float bulletRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bulletRotation + 90f);
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
            DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage, isCritical);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
