using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.Mathematics;
using UnityEngine;
using static PlayerDamage;

public class PlayerDamage : MonoBehaviour
{
    private Health enemyHealth;
    public Player player;

    #region Damage Formula

    [Header("Damage Formula")]

    [Tooltip("Base damage = (Talent% * ATK)")]
    public int talentLevel = 1;
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

    #endregion Damage Formula


    #region Derildo numbers
    // Enum
    public enum DamageType
    {
        NormalAttack,
        ChargedAttack,
        Skill,
        Ult,
    }

    // Lists
    /*
    public List<float> normalAttackMultipliers = new List<float>();
    public List<float> normalAttackTalentLevel = new List<float>();

    public List<int> talentLevelList = new List<int>();
    */
    // talents

    // talent lvl
    // attack string multiplers 
    // attack string 01
    // attack string 02
    // attack string 03


    // attack type
    // talent lvl
    // current atk string

    [Header("New Talents")]
    public int basicAttackTalentLevel = 1;

    public float basicAttack_TalentLevel1_String01 = 0.4455f;
    public float basicAttack_TalentLevel1_String02 = 0.4247f;
    public float basicAttack_TalentLevel1_String03 = 0.5461f;

    public float basicAttack_TalentLevel2_String01 = 0.4817f;
    public float basicAttack_TalentLevel2_String02 = 0.4622f;
    public float basicAttack_TalentLevel2_String03 = 0.5906f;




    #endregion Derildo numbers



    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
    }


    private void Start()
    {
        damageTrigger = false;

        /*
        normalAttackMultipliers.Add(0.4455f);
        normalAttackMultipliers.Add(0.4817f);
        normalAttackMultipliers.Add(0.518f);
        normalAttackMultipliers.Add(0.5698f);
        normalAttackMultipliers.Add(0.6061f);
        normalAttackMultipliers.Add(0.6475f);

        normalAttackTalentLevel.Add(normalAttackMultipliers[1]);
        normalAttackTalentLevel.Add(normalAttackMultipliers[2]);
        normalAttackTalentLevel.Add(normalAttackMultipliers[3]);
        normalAttackTalentLevel.Add(normalAttackMultipliers[4]);
        normalAttackTalentLevel.Add(normalAttackMultipliers[5]);

        talentLevelList.Add(1);
        Debug.Log("Talent level list:" + talentLevelList[0]);
        */

    }

    private void Update()
    {

        /*
        for (int i = 0; i < normalAttackMultipliers.Count; i++)
        {
            Debug.Log("List " + i);
        }*/

        if (Input.GetButtonDown("Fire1"))
        {
            damageTrigger = true;
            CalculateDamageFormulaCRIT(DamageType.NormalAttack);

        }

        if (Input.GetButtonDown("Skill"))
        {
            //CalculateDamageFormula(DamageType.Skill);
            CalculateDamageFormulaCRIT(DamageType.Skill);
            FindObjectOfType<AudioManager>().PlaySound("PlayerSkill");




        }


        if (Input.GetButtonDown("Ult"))
        {
            //CalculateDamageFormula(DamageType.Ult);
            CalculateDamageFormulaCRIT(DamageType.Ult);

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

    public void CalculateDamageFormulaCRIT(DamageType damageType)
    {


        /*
        if (talentLevel == 1)
        {
            talentMultiplier = normalAttackMultipliers[0];
        }
        else if (talentLevel == 2)
        {
            talentMultiplier = normalAttackMultipliers[1];
        }
        else if (talentLevel == 3)
        {
            talentMultiplier = normalAttackMultipliers[2];
        }
        else if (talentLevel == 4)
        {
            talentMultiplier = normalAttackMultipliers[3];
        }
        else if (talentLevel == 5)
        {
            talentMultiplier = normalAttackMultipliers[4];
        }
        else if (talentLevel == 6)
        {
            talentMultiplier = normalAttackMultipliers[5];
        }
       
        */

        switch (damageType)
        {
            case DamageType.NormalAttack:
                Debug.Log("Normal attack");
                if (basicAttackTalentLevel == 1)
                {
                    if(player.currentAttack != 0)
                    {
                        if (player.currentAttack == 1)
                        {
                            talentMultiplier = basicAttack_TalentLevel1_String01;

                        }

                        else if (player.currentAttack == 2)
                        {
                            talentMultiplier = basicAttack_TalentLevel1_String02;

                        }

                        else if (player.currentAttack == 3)
                        {
                            talentMultiplier = basicAttack_TalentLevel1_String03;

                        }
                    }
                    





                }
                else if (basicAttackTalentLevel == 2)
                {
                    talentMultiplier = basicAttack_TalentLevel2_String01;
                }

                break;
            case DamageType.ChargedAttack:
                talentMultiplier = 0.8125f;
                break;
            case DamageType.Skill:
                talentMultiplier = 1.9264f;
                break;
            case DamageType.Ult:
                talentMultiplier = 3.2592f;
                break;
        }

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

    public void CalculateDamageFormula(DamageType damageType)
    {

        switch (damageType)
        {
            case DamageType.NormalAttack:
                talentMultiplier = 0.6475f;
                break;
            case DamageType.ChargedAttack:
                talentMultiplier = 0.8125f;
                break;
            case DamageType.Skill:
                talentMultiplier = 1.9264f;
                break;
            case DamageType.Ult:
                talentMultiplier = 3.2592f;
                break;
        }

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
