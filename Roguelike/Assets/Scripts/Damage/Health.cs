using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 200;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

}
