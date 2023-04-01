using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public float CurrentDamage { get; set; }
    public bool isCritical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformAttack(float damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("Base_Attack");
        //Debug.Log(this.name + " basic attack!");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
            DamagePopup.Create(transform.position, (int)CurrentDamage, isCritical);
            Debug.Log("Hit: " + collision.name);
        }

    }
}
