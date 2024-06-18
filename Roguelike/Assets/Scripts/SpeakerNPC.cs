using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerNPC : NPC
{
    private void Start()
    {
        DialogueSystem.Instance.OnDialogueFinished += DialogueSystem_OnDialogueFinished;
    }

    private void DialogueSystem_OnDialogueFinished(object sender, System.EventArgs e)
    {
        if (!IsInDialogue) return;

        DestroyGameObject();
    }

    private void OnDestroy()
    {
        DialogueSystem.Instance.OnDialogueFinished -= DialogueSystem_OnDialogueFinished;
    }
}
