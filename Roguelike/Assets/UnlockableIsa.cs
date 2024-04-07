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
        OnCharacterUnlocked.Instance.UnlockIsaUpdatingState();
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!OnCharacterUnlocked.Instance.IsIsaUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockIsaInvokingEvent();
            Debug.Log("Isa has been unlocked");
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnIsaUnlocked -= OnCharacterUnlocked_OnIsaUnlocked;
    }
}
