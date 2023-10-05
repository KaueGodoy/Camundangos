using UnityEngine;

public class Staff : Weapon, IProjectileWeapon
{
    public Transform ProjectileSpawn { get; set; }
    private Fireball _fireball;

    public override void Awake()
    {
        base.Awake();
        _fireball = Resources.Load<Fireball>("Projectiles/Fireball");
    }

    public override void PerformAttack(float damage)
    {
        base.PerformAttack(damage);
        Animator.SetTrigger("Base_Attack");
    }

    public void CastProjectile()
    {
        Fireball fireballInstance = (Fireball)Instantiate(_fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.right;
    }
}
