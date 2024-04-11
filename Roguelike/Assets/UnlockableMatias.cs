using UnityEngine;

public class UnlockableMatias : UnlockableCharacter
{
    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked += OnCharacterUnlocked_OnMatiasUnlocked;
    }

    private void OnCharacterUnlocked_OnMatiasUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.UnlockMatiasUpdatingState();
        InvokeMatiasUnlockedVisual();
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        if (!OnCharacterUnlocked.Instance.IsMatiasUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockMatiasInvokingEvent();
            Debug.Log("Matias has been unlocked");

            DestroyGameObjectWithDelay(TimeToDestroySelf);
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked -= OnCharacterUnlocked_OnMatiasUnlocked;
    }
}
