using System.Collections;
using UnityEngine;

public class UnlockableLeo : NPC
{
    [SerializeField] private float _timeToDestroy = 0.3f;

    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnLeoUnlocked += OnCharacterUnlocked_OnLeoUnlocked;
    }

    private void OnCharacterUnlocked_OnLeoUnlocked(object sender, System.EventArgs e)
    {
        OnCharacterUnlocked.Instance.UnlockLeoUpdatingState();
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        if (!OnCharacterUnlocked.Instance.IsLeoUnlocked)
        {
            OnCharacterUnlocked.Instance.UnlockLeoInvokingEvent();
            Debug.Log("Leo has been unlocked");

            //OnCharacterUnlocked.Instance.InvokeOnCharacterUnlockedVisualUpdate();
            DestroyGameObjectWithDelay(_timeToDestroy);
        }
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
        OnCharacterUnlocked.Instance.OnLeoUnlocked -= OnCharacterUnlocked_OnLeoUnlocked;
    }
}
