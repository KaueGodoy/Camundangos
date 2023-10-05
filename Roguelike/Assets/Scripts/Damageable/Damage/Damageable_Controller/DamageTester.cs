using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public float DamageAmount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(DamageAmount);
            Debug.Log("Dealing damage: " + DamageAmount);
        }

    }
}
