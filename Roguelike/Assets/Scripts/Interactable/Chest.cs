using UnityEngine;

public class Chest : Interactable
{
    public override void InitiateInteraction()
    {
        base.InitiateInteraction();
        Debug.Log("Interacting with chest");
    }
}
