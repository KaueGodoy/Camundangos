using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{

    [Header("Burning")]
    public float tickDamage = 4f;
    public float tickSpeed = 0.75f;

    public List<float> burnTickTimers = new List<float>();

    public void ApplyBurn(float ticks)
    {
            if (burnTickTimers.Count <= 0)
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
        
    }

    IEnumerator Burn()
    {
        while (burnTickTimers.Count > 0)
        {
            for (int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }

            burnTickTimers.RemoveAll(i => i == 0);

          
            yield return new WaitForSeconds(tickSpeed);

        }
    }
}
