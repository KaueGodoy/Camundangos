using UnityEngine;

public class BaseEnemy : DamageableWithHealthBar
{
    public static BaseEnemy Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public virtual void AttackPlayer(float damage)
    {
        NewPlayerController.Instance.TakeDamage(damage);
    }
}
