using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public Transform ProjectileSpawn { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public float CurrentDamage { get; set; }

    Fireball fireball;

    private void Awake()
    {
        fireball = Resources.Load<Fireball>("Projectiles/Fireball");
        animator = GetComponent<Animator>();
    }

    public void PerformAttack(float damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("Base_Attack");
    }

    public void PerformSkillAttack()
    {
        //animator.SetTrigger("Skill_Attack");
    }

    public void PerformUltAttack()
    {
        //animator.SetTrigger("Ult_Attack");
    }

    public void CastProjectile()
    {
        Fireball fireballInstance = (Fireball)Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.right;
    }

}
