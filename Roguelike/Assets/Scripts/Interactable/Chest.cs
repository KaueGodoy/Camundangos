using UnityEngine;

public class Chest : Interactable
{
    public override void Awake()
    {
        base.Awake();
        SetName("Chest");
    }

    public override void InitiateInteraction()
    {
        base.InitiateInteraction();
        Debug.Log("Interacting with chest");
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Debug.Log("Chest claimed");
    }
}
