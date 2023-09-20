using UnityEngine;

public class Chest : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with chest");
    }
}
