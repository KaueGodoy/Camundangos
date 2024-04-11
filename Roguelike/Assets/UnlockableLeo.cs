using System;
using System.Collections;
using UnityEngine;

public class UnlockableLeo : UnlockableCharacter
{
    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnLeoUnlocked += OnCharacterUnlocked_OnLeoUnlocked;
    }

    private void OnCharacterUnlocked_OnLeoUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.UnlockLeoUpdatingState();
        InvokeLeoUnlockedVisual();  
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        if (!OnCharacterUnlocked.Instance.IsLeoUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockLeoInvokingEvent();
            Debug.Log("Leo has been unlocked");

            DestroyGameObjectWithDelay(TimeToDestroySelf);
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnLeoUnlocked -= OnCharacterUnlocked_OnLeoUnlocked;
    }
}
