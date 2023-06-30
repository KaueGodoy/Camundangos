using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string name;

    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, name);
        Debug.Log("Interacting with NPC class");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact();
    }
}
