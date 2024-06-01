using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    public event EventHandler OnDialogueFinished;

    public GameObject dialoguePanel;
    [SerializeField] private DialoguePanelVisual _dialoguePanelVisual;

    public string npcName;
    public List<string> dialogueLines;

    [SerializeField] private Button _continueButton;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _nameText;

    int dialogueIndex;
    private bool _isInDialogue = false;

    private void Awake()
    {
        _continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;
    }

    private void GameInput_OnPlayerInteract(object sender, System.EventArgs e)
    {
        if (!_isInDialogue) return;

        ContinueDialogue();
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;

        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.npcName = npcName;

        Debug.Log(dialogueLines.Count);

        CreateDialogue();
    }

    public void CreateDialogue()
    {
        _dialogueText.text = dialogueLines[dialogueIndex];
        _nameText.text = npcName;
        dialoguePanel.SetActive(true);
        _dialoguePanelVisual.OnDialogueCreatedPlayAnimation();
        _isInDialogue = true;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            _dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            HideDialogue();
            OnDialogueFinished?.Invoke(this, EventArgs.Empty);
            _isInDialogue = false;
        }
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerInteract -= GameInput_OnPlayerInteract;
    }

}
