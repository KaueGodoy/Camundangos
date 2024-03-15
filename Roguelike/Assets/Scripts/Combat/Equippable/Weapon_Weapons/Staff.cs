using UnityEngine;

public class Staff : Weapon, IProjectileWeapon
{
    public Transform ProjectileSpawn { get; set; }
    public Fireball Fireball { get; set; }

    public override void Awake()
    {
        base.Awake();
        
        Fireball = Resources.Load<Fireball>("Projectiles/Fireball");
    }

    public override void PerformAttack(float damage)
    {
        base.PerformAttack(damage);
        Animator.SetTrigger("Base_Attack");
    }

    public void CastProjectile()
    {
        Fireball fireballInstance = Instantiate(Fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.right;
    }
}
