using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    public float moveSpeed;
    public float timeToDestroy = 2f;

    private void Start()
    {
        targetPosition = FindObjectOfType<Player>().transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
