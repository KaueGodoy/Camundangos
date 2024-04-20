using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public Animator Animator { get; set; }
    public List<BaseStat> Stats { get; set; }
    public float CurrentDamage { get; set; }

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void PerformAttack(float damage)
    {
        CurrentDamage = damage;
        AudioManager.Instance.PlaySound("PlayerWeaponAttack");
        //Debug.Log(this.name + " basic attack!");
    }

    public virtual void PerformSkillAttack()
    {
        Debug.Log(this.name + " skill attack!");
    }

    public virtual void PerformUltAttack()
    {
        Debug.Log(this.name + " ult attack!");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(CurrentDamage);
            Debug.Log($"Dealing {CurrentDamage} damage to {collision.name}");
            AudioManager.Instance.PlaySound("Hitmarker");
        }
    }

    public virtual void PlayWeaponAudio(string sfx)
    {
        AudioManager.Instance.PlaySound(sfx);
    }

}
