using System;
using System.Collections.Generic;
using UnityEngine;

public class Box : Destructible
{
    public event EventHandler DropLootOnDestroy;

    public static Box Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        DropLootOnDestroy?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);  
    }
}
