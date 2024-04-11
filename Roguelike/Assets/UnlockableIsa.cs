using UnityEngine;

public class UnlockableIsa : UnlockableCharacter
{
    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnIsaUnlocked += OnCharacterUnlocked_OnIsaUnlocked;
    }

    private void OnCharacterUnlocked_OnIsaUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.UnlockIsaUpdatingState();
        InvokeIsaUnlockedVisual();  
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        if (!OnCharacterUnlocked.Instance.IsIsaUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockIsaInvokingEvent();
            Debug.Log("Isa has been unlocked");
            DestroyGameObjectWithDelay(TimeToDestroySelf);
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnIsaUnlocked -= OnCharacterUnlocked_OnIsaUnlocked;
    }
}
