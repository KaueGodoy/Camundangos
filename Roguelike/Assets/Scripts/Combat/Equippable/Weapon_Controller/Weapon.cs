using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public Animator Animator { get; set; }
    public List<BaseStat> Stats { get; set; }
    public float CurrentDamage { get; set; }

    [SerializeField] private ParticleSystem _attackParticle;
    public ParticleSystem AttackParticle { get { return _attackParticle; } set { _attackParticle = value; } }

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }

   

    public virtual void PerformAttack(float damage)
    {
        //if (PlayerAttack.Instance.AttackRequest) return;
        CurrentDamage = damage;
        AudioManager.Instance.PlaySound("PlayerWeaponAttack");
        ParticleManager.Instance.InstantiateParticle(AttackParticle, transform.position);
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
