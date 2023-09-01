using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool isCritical;


    public CharacterStats characterStats { get; set; }

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
        // check for collision
        StatusEffectManager enemy = collision.GetComponent<StatusEffectManager>();
        PlayerDamage playerDamage = collision.GetComponent<PlayerDamage>();
        if (enemy != null)
        {


            FindObjectOfType<AudioManager>().PlaySound("Hitmarker");


            enemy.ApplyProjectileDamage(projectileDamage);
            //Debug.Log("Initial damage " + projectileDamage);
            enemy.ApplyBurn(burnAmount);
            //Debug.Log("Burning " + burnAmount);
            //playerDamage.CalculateDamageFormulaCRIT();
            Debug.Log("NEW DAMAGE FORMULA");

        }

        // check for enemy HP
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            if (health.currentHealth <= 0)
            {
                Destroy(collision.gameObject);
            }
        }

        // check collision with player - not destryoing the bullet
        PlayerController player = collision.GetComponent<PlayerController>();
        if (!player)
        {
            Destroy(gameObject);

        }

        if(collision.tag == "Enemy")
        {
            //Debug.Log("Enemy hit");
            collision.GetComponent<IEnemy>().TakeDamage(projectileDamage);
            DamagePopup.Create(transform.position, (int)projectileDamage, isCritical);

            //collision.GetComponent<IEnemy>().TakeDamage(CharacterStats.GetStat(BaseStat.BaseStatType.Attack).GetCalculatedStatValue());
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
