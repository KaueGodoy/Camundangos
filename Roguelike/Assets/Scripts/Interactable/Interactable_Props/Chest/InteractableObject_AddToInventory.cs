using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_AddToInventory : Interactable
{
    [SerializeField] private string _name;
    [SerializeField] private List<string> drops = new List<string>();

    public override void Awake()
    {
        base.Awake();
        Name = _name;
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
        DestroyGameObjectWithDelay(0.3f);
    }

    public virtual void OpenChest()
    {
        if (Drops == null) return;

        foreach (var item in Drops)
        {
            GiveItemToPlayer(item);
        }

        AudioManager.PlaySound("OnChestOpen");
    }
}
