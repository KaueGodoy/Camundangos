using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StatusEffectManager : MonoBehaviour
{
    private Health healthScript;

    [Header("Burning")]
    public float tickDamage = 4f;
    public float tickSpeed = 0.75f;

    public List<float> burnTickTimers = new List<float>();


    private void Awake()
    {
        healthScript = GetComponent<Health>();
    }

    public void ApplyBurn(float ticks)
    {
        if(burnTickTimers.Count <= 0)
        {
            burnTickTimers.Add(ticks);
            StartCoroutine(Burn());
        }
        else
        { 
            burnTickTimers.Add(ticks);
        }
    }

    public void ApplyProjectileDamage(float damage)
    {
        healthScript.currentHealth -= damage;
    }

    IEnumerator Burn()
    {
        while (burnTickTimers.Count > 0)
        {
            for (int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }

            healthScript.currentHealth -= tickDamage;
            burnTickTimers.RemoveAll(i => i == 0);

            // check if enemy is dead
            if (healthScript.currentHealth <= 0)
            {
                Destroy(healthScript.gameObject);
            }

            yield return new WaitForSeconds(tickSpeed);

        }
    }
}
