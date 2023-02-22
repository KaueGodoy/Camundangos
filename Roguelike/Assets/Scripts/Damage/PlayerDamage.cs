using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Base Stats")]
    public float baseDamage = 2f;
    public float flatDamage = 4f;
    [Space]
    public float baseCritRate = 5f; // 5%
    public float baseCritDamage = 50f; // 50%

    [Header("Current Stats")]
    public float currentCritRate = 45f;
    public float currentCritDamage = 50f;

    [Header("Final Stats")]
    public float finalCritRate;
    public float finalCritDamage;
    public float finalDamage;

    public bool isCrit;
    public bool damageTrigger;

    private void Start()
    {
        damageTrigger = false;
        
        finalCritRate = baseCritRate + currentCritRate;
        finalCritDamage = baseCritDamage + currentCritDamage;
        finalDamage = baseDamage + flatDamage;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            damageTrigger = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
           CritDamageBuff();
          

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


    public void CalculateDamage()
    {
        finalCritRate = baseCritRate + currentCritRate;
        finalCritDamage = baseCritDamage + currentCritDamage;

        isCrit = UnityEngine.Random.Range(0, 100) < finalCritRate;

        if (!isCrit)
        {
            finalDamage = (baseDamage + flatDamage);
        }
        else if (isCrit)
        {
            finalDamage = (baseDamage + flatDamage) * finalCritDamage;
        }

        Debug.Log("Damage: " + finalDamage);

    }

    public void CritDamageBuff()
    {
        currentCritDamage += 20f;
        Debug.Log("Crit damage increase: " + finalDamage);

    }
}
