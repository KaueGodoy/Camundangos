using UnityEngine;

public class EnemyNewSystem : MonoBehaviour, IDamageable
{
    public float CurrentHealth;
    public float MaxHealth = 1000f;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed: " + gameObject.name);
        }

    }


}
