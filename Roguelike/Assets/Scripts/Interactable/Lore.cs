using UnityEngine;

public class Lore : Interactable
{
    public override void Awake()
    {
        base.Awake();
        SetName("Lore");
    }

    public override void InitiateInteraction()
    {
        base.InitiateInteraction();
        Debug.Log("This is a piece of lore");
    }
}
