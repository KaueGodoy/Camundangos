using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string _name;

    public override void InitiateInteraction()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, this._name);

        AudioManager.Instance.PlaySound("OnTutorialShowUp");
        Debug.Log("Interacting with NPC class");
    }

    public override void DisableInteraction()
    {
        DialogueSystem.Instance.HideDialogue();
    }

    //public override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    InitiateInteraction();
    //}
}
