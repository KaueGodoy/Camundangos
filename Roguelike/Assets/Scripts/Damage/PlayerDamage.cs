using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private Health enemyHealth;

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

    [Header("Enemy Resistance")]
    [Tooltip("Resistance = base resistance - resistance reduction")]
    public float resistance;

    [Tooltip("The enemy's base resistance to the Element of the attack being used / 10% base")]
    public float baseResistance;

    [Tooltip("The total resistance reduction of the relevant Element from effects such as SC and VV / -40% VV effect")]
    public float resistanceReduction;
    
    [Tooltip("The enemy resistance multiplier based on different resistance stats")]
    public float enemyResMult;

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

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
    }

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

        resistance = baseResistance - resistanceReduction;

        if(resistance < 0)
        {
            enemyResMult = (1 - (resistance / 2)); 
        }
        else if (resistance >= 0 && resistance < 0.75f)
        {
            enemyResMult = (1 - resistance);

        }
        else if(resistance >= 0.75f)
        {
            enemyResMult = (1 / (4 * resistance + 1));
        }



        damage = ((baseDamage) + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage * enemyDefMult * enemyResMult;
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
            FindObjectOfType<AudioManager>().PlaySound("PlayerSkill");




        }


        if (Input.GetButtonDown("Ult"))
        {
            CalculateDamageFormulaCRIT();

            FindObjectOfType<AudioManager>().PlaySound("PlayerUlt");


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

        resistance = baseResistance - resistanceReduction;

        if (resistance < 0)
        {
            enemyResMult = (1 - (resistance / 2));
        }
        else if (resistance >= 0 && resistance < 0.75f)
        {
            enemyResMult = (1 - resistance);

        }
        else if (resistance >= 0.75f)
        {
            enemyResMult = (1 / (4 * resistance + 1));
        }

        rawDamage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage;

        damage = ((baseDamage) + flatDamage) * (1 + damageBonus - damageReduction) * CRITDamage * enemyDefMult * enemyResMult;

        Debug.Log("Damage result: " + damage);

        enemyHealth.currentHealth -= damage;
        
    }

    public void CalculateDamageFormula()
    {

        attack = (attackCharacter + attackWeapon) * (1 + attackBonus) + flatAttack;
        baseDamage = talentMultiplier * attack;


        enemyDefMult = (levelCharacter + 100) /
                       ((levelCharacter + 100) + (levelEnemy + 100) * (1 - defReduction) * (1 - defIgnore));

        rawDamage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction);

        damage = (baseDamage + flatDamage) * (1 + damageBonus - damageReduction) * enemyDefMult * enemyResMult;

        Debug.Log("Damage result: " + damage);

        enemyHealth.currentHealth -= damage;
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
