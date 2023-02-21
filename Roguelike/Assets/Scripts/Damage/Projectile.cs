using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Damage")]
    public float projectileDamage = 6f;
    
    [Header("Burning")]
    public float burnAmount = 4f;

    [Header("Speed")]
    public float projectileSpeed = 5f;

    [Header("Distance")]
    public float projectileDistance = 2f;

    private void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    private void Update()
    {
        Destroy(this.gameObject, projectileDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StatusEffectManager enemy = collision.GetComponent<StatusEffectManager>();
        if(enemy != null)
        {
          
            enemy.ApplyProjectileDamage(projectileDamage);
            Debug.Log("Initial damage " + projectileDamage);
            enemy.ApplyBurn(burnAmount);
            Debug.Log("Burning " + burnAmount);
        }

        

        Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
