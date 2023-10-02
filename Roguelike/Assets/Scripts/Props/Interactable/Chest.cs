using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
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
        DestroyGameObject();
    }

    public virtual void OpenChest()
    {
        if (Drops == null) return;

        foreach (var item in Drops)
        {
            GiveItemToPlayer(item);
        }
    }
}
