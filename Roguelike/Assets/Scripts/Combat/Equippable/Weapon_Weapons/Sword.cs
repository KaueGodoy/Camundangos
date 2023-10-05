using UnityEngine;

public class Sword : Weapon
{
    public override void PerformAttack(float damage)
    {
        base.PerformAttack(damage);
        Animator.SetTrigger("Base_Attack");
    }
}
