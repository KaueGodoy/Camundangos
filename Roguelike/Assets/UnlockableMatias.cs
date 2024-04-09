using UnityEngine;

public class UnlockableMatias : NPC
{
    [SerializeField] private float _timeToDestroy = 0.3f;

    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked += OnCharacterUnlocked_OnMatiasUnlocked;
    }

    private void OnCharacterUnlocked_OnMatiasUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.UnlockMatiasUpdatingState();
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        if (!OnCharacterUnlocked.Instance.IsMatiasUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockMatiasInvokingEvent();
            Debug.Log("Marcelo has been unlocked");

            //OnCharacterUnlocked.Instance.InvokeOnCharacterUnlockedVisualUpdate();
            DestroyGameObjectWithDelay(_timeToDestroy);
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked -= OnCharacterUnlocked_OnMatiasUnlocked;
    }
}
