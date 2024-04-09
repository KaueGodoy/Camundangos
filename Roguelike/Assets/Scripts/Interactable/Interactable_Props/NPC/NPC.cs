using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string _name;

    public bool IsInDialogue { get; private set; }  

    public override void InitiateInteraction()
    {
        IsInDialogue = true;
        DialogueSystem.Instance.AddNewDialogue(dialogue, this._name);

        AudioManager.Instance.PlaySound("OnTutorialShowUp");
        Debug.Log("Interacting with NPC class");
    }

    public override void DisableInteraction()
    {
        IsInDialogue = false;
        DialogueSystem.Instance.HideDialogue();
    }
}
