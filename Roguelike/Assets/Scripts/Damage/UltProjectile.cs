using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UltProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Damage")]
    public float projectileDamage = 6f;

    [Header("Speed")]
    public float projectileSpeed = 5f;

    [Header("Distance")]
    public float projectileDistance = 2f;

    public CharacterStats characterStats { get; set; }

    private void Start()
    {
        //rb.velocity = transform.right * projectileSpeed;
        rb.velocity = new Vector2(projectileSpeed, -projectileSpeed);

    }

    private void Update()
    {
        Destroy(this.gameObject, projectileDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<IEnemy>().TakeDamage(projectileDamage);
            DamagePopup.Create(transform.position, (int)projectileDamage);
            FindObjectOfType<AudioManager>().PlaySound("Hitmarker");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
