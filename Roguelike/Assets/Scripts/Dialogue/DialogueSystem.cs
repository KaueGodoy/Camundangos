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

    public string npcName;
    public List<string> dialogueLines;

    [SerializeField] private Button _continueButton;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _nameText;

    //buttonText;
    int dialogueIndex;

    private void Awake()
    {
        //buttonText = dialoguePanel.transform.Find("Button/Continue").GetComponent<TMP_Text>();
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
        //buttonText.text = "Continue";
        dialoguePanel.SetActive(true);
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
            //buttonText.text = "End";
            HideDialogue();
            OnDialogueFinished?.Invoke(this, EventArgs.Empty);
        }
    }

}
