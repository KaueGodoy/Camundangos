using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public event EventHandler OnChestOpenedParticles;

    [SerializeField] private List<string> drops = new List<string>();

    public override void Awake()
    {
        base.Awake();
        SetName("Chest");
    }

    public List<string> Drops
    {
        get { return drops; }
        set { drops = value; }
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        OpenChest();
        DestroyGameObjectWithDelay(0.4f);
    }

    public virtual void OpenChest()
    {
        if (Drops == null) return;

        foreach (var item in Drops)
        {
            GiveItemToPlayer(item);
        }

        AudioManager.PlaySound("OnChestOpen");
        OnChestOpenedParticles?.Invoke(this, EventArgs.Empty);
    }
}
