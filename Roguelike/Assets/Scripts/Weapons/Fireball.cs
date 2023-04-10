using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Range { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }

    public bool isCritical;

    Vector2 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        Range = 10f;
        Speed = 70f;
        Damage = 250f;
        GetComponent<Rigidbody2D>().AddForce(Direction * Speed);

    }

    private void Update()
    {
        if (Vector2.Distance(spawnPosition, transform.position) >= Range)
        {
            Extinguish();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IEnemy>().TakeDamage(Damage);
            DamagePopup.Create(transform.position, (int)Damage, isCritical);

            Debug.Log("Hit: " + collision.name);
        }

        Player player = collision.GetComponent<Player>();
        if (!player)
        {
            Extinguish();
        }

    }

    void Extinguish()
    {
        Destroy(gameObject);
    }

}
