using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableIsa : NPC
{
    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnIsaUnlocked += OnCharacterUnlocked_OnIsaUnlocked;
    }

    private void OnCharacterUnlocked_OnIsaUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.IsIsaLocked = false;
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        // unlock her to join the party and be played
        // is unlocked = true
        // change player icon from black to her icon 
        // play audio
        // animation of unlocking her 
        OnCharacterUnlocked.Instance.UnlockIsa();
    }
}
