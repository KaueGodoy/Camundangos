public class Enemy_SkeletonSelfRepair : Damageable
{
    public override void Die()
    {
        CurrentHealth = MaxHealth;
    }
}
