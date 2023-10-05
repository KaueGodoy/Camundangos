using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    public GameObject dialoguePanel;

    public string npcName;
    public List<string> dialogueLines;

    Button continueButton;
    TMP_Text dialogueText, nameText, buttonText;
    int dialogueIndex;

    private void Awake()
    {
        continueButton = dialoguePanel.transform.Find("Button").GetComponent<Button>();
        //buttonText = dialoguePanel.transform.Find("Button/Continue").GetComponent<TMP_Text>();
        dialogueText = dialoguePanel.transform.Find("TextBox").GetComponent<TMP_Text>();
        nameText = dialoguePanel.transform.Find("Name/Text").GetComponent<TMP_Text>();

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

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
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        //buttonText.text = "Continue";
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            //buttonText.text = "End";
            dialoguePanel.SetActive(false);
        }
    }

}
