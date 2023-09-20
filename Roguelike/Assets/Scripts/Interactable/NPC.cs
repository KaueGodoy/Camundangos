using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string _name;

    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, this._name);
        Debug.Log("Interacting with NPC class");
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Interact();
    }
}
