using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Damage Formula")]
 
    [Tooltip("Base damage = (Talent% * ATK)")]
    public float baseDamage;
    public float talentMultiplier;

    [Header("Additive Damage")]
    [Tooltip("Comes after the ATK calculation")]
    public float flatDamage;

    [Header("Attack")]

    [Tooltip("ATK = (ATK Character + ATK Weapon) * (1 + attackBonus) + Flat ATK")]
    public float attack;

    [Tooltip("Character's Base ATK")]
    public float attackCharacter;
    
    [Tooltip("Weapon's Base ATK")]
    public float attackWeapon;
    
    [Tooltip("Sum of all percentage-based ATK Bonuses from weapons, artifacts, and other sources")]
    public float attackBonus;
    
    [Tooltip("Sum of all non-percentage-based ATK Bonuses from artifacts, and other sources")]
    public float flatAttack;

    [Header("Elemental Bonus")]
    
    [Tooltip("Sum of all percentage damage increases from goblets, weapons, set bonuses and other buffs")]
    public float damageBonus;

    [Tooltip("is an attribute that directly reduces the Percentage Damage Bonus of incoming attacks. It can be understood as a negative Percentage Damage Bonus")]
    public float damageReduction;

    [Header("Crit")]
    public float builtCritRate;
    public float builtCritDamage;
    [Space]
    public float CRITRate;
    public float CRITDamage;
    [Space]   
    private const float BASE_CRIT_RATE = 1 + 0.05f; // 5%
    private const float BASE_CRIT_DAMAGE = 1 + 0.50f; // 50%

    [Header("Enemy Defense")]
    
    [Tooltip("The player character's level")]
    public float levelCharacter;

    [Tooltip("The enemy's level")]
    public float levelEnemy;

    [Tooltip("The total defense (but not resistance) reduction from various defense reduction effects.")]
    public float defReduction;

    [Tooltip("The total defense ignore from effects such as Raiden C2")]
    public float defIgnore;

    [Tooltip("The enemy defense multiplier")]
    public float enemyDefMult;

    [Header("Final Damage")]

    [Tooltip("Damage before enemy def and res")]
    public float rawDamage;
    
    [Tooltip("Damage = (Base damage) + Flat damage")]
    public float damage;



    [Header("Final Stats")]
    public float finalCritRate;
    public float finalCritDamage;
    public float finalDamage;

    public bool isCrit;
    public bool damageTrigger;


    private void Start()
    {
        damageTrigger = false;
        
        finalCritRate = BASE_CRIT_RATE + builtCritRate;
        finalCritDamage = BASE_CRIT_DAMAGE + builtCritDamage;


        finalDamage = baseDamage + flatDamage;



        attack = (attackCharacter + attackWeapon) * (1 + attackBonus) + flatAttack;
        baseDamage = talentMultiplier * attack;

        CRITRate = BASE_CRIT_RATE + builtCritRate;
        CRITDamage = BASE_CRIT_DAMAGE + builtCritDamage;

        enemyDefMult =                          (levelCharacter + 100) / 
                       ((levelCharacter +100) + (levelEnemy + 100) * (1 - defReduction) * (1 - defIgnore));

        damage = ((baseDamage) + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage * enemyDefMult;
    }

    private void Update()
    {
        //Time.timeScale = 1f + timeChanger;


        if (Input.GetButtonDown("Fire1"))
        {
            damageTrigger = true;
        }

        if (Input.GetButtonDown("Skill"))
        {
            CalculateDamageFormula();



        }


        if (Input.GetButtonDown("Ult"))
        {
            CalculateDamageFormulaCRIT();



        }
    }

    private void FixedUpdate()
    {
        ApplyDamage();
    }

    public void ApplyDamage()
    {
        if (damageTrigger)
        {
            //CritDamageBuff();
            CalculateDamage();
        }

        damageTrigger = false;
    }

    public void CalculateDamageFormulaCRIT()
    {

        isCrit = UnityEngine.Random.Range(0, 100) < CRITRate;

        attack = (attackCharacter + attackWeapon) * (1 + attackBonus) + flatAttack;
        baseDamage = talentMultiplier * attack;

        CRITRate = BASE_CRIT_RATE + builtCritRate;
        CRITDamage = BASE_CRIT_DAMAGE + builtCritDamage;

        enemyDefMult = (levelCharacter + 100) /
                       ((levelCharacter + 100) + (levelEnemy + 100) * (1 - defReduction) * (1 - defIgnore));

        rawDamage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage;

        damage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage * enemyDefMult;

        Debug.Log("Damage result: " + damage);
    }

    public void CalculateDamageFormula()
    {

        attack = (attackCharacter + attackWeapon) * (1 + attackBonus) + flatAttack;
        baseDamage = talentMultiplier * attack;


        enemyDefMult = (levelCharacter + 100) /
                       ((levelCharacter + 100) + (levelEnemy + 100) * (1 - defReduction) * (1 - defIgnore));

        rawDamage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction);

        damage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction) * enemyDefMult;

        Debug.Log("Damage result: " + damage);
    }


    public void CalculateDamage()
    {
        finalCritRate = BASE_CRIT_RATE + builtCritRate;
        finalCritDamage = BASE_CRIT_DAMAGE + builtCritDamage;

        isCrit = UnityEngine.Random.Range(0, 100) < finalCritRate;

        if (!isCrit)
        {
            finalDamage = (baseDamage + flatDamage);
        }
        else if (isCrit)
        {
            finalDamage = (baseDamage + flatDamage) * finalCritDamage;
        }

        //Debug.Log("Damage: " + finalDamage);

    }

    public void CritDamageBuff()
    {
        builtCritDamage += 20f;
        //Debug.Log("Crit damage increase: " + finalDamage);

    }
}
