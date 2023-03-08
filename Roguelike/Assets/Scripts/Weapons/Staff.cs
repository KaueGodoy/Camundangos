using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public Transform ProjectileSpawn { get; set; }

    Fireball fireball;


    private void Awake()
    {
        fireball = Resources.Load<Fireball>("Projectiles/Fireball");
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        animator.SetTrigger("Base_Attack");
        Debug.Log(this.name + " basic staff attack!");
    }

    public void PerformSkillAttack()
    {
        animator.SetTrigger("Skill_Attack");
        Debug.Log(this.name + " skill attack!");
    }

    public void PerformUltAttack()
    {
        animator.SetTrigger("Ult_Attack");
        Debug.Log(this.name + " ult attack!");
    }

    public void CastProjectile()
    {
        Fireball fireballInstance = (Fireball)Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.right;
    }
}
