using System;
using UnityEngine;

public abstract class Destructible : MonoBehaviour, IDamageable
{
    public virtual void TakeDamage(float amount)
    {
        DamagePopup.Create(transform.position, (int)amount);
        Debug.Log("Object destroyed: " + this.gameObject.name);
    }

}
