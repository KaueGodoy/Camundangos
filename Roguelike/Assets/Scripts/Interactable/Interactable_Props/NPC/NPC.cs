using System;
using UnityEngine;

public class NPC : Interactable
{
    public string _name;
    public string[] dialogue;

    [SerializeField] private TextAsset _file;

    private void OnValidate()
    {
        LoadDialogueFile();
    }

    private void LoadDialogueFile()
    {
        dialogue = _file ? _file.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
    }

    public bool IsInDialogue { get; private set; }  

    public override void InitiateInteraction()
    {
        IsInDialogue = true;
        AudioManager.Instance.PlaySound("OnTutorialShowUp");

        DialogueSystem.Instance.AddNewDialogue(dialogue, this._name);

        Debug.Log("Interacting with NPC class");
    }

    public override void DisableInteraction()
    {
        IsInDialogue = false;
        DialogueSystem.Instance.HideDialogue();
    }
}
