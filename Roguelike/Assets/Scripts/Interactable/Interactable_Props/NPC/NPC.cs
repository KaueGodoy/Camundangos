using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class NPC : Interactable
{
    public string _name;
    public string[] dialogue;

    [SerializeField] private TextAsset _englishText;
    [SerializeField] private TextAsset _brazilianPortugueseText;
    [SerializeField] private TextAsset _japaneseText;
    [SerializeField] private TextAsset _spanishText;

    private int _englishLocaleIndex = 0;
    private int _brazilianPortugueseLocaleIndex = 1;
    private int _JapaneseLocaleIndex = 2;
    private int _SpanishLocaleIndex = 3;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
    }

    private void OnSelectedLocaleChanged(Locale newLocale)
    {

    }

    private void LoadDialogueFile()
    {
        if (LocalizationSettings.SelectedLocale == null)
        {
            Debug.LogWarning("Selected locale is null.");
            return;
        }

        // Load dialogue based on the selected locale
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_englishLocaleIndex])
        {
            dialogue = _englishText ? _englishText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_brazilianPortugueseLocaleIndex])
        {
            dialogue = _brazilianPortugueseText ? _brazilianPortugueseText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_JapaneseLocaleIndex])
        {
            dialogue = _japaneseText ? _japaneseText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_SpanishLocaleIndex])
        {
            dialogue = _spanishText ? _spanishText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
        }
    }

    public bool IsInDialogue { get; private set; }

    public override void InitiateInteraction()
    {
        IsInDialogue = true;
        AudioManager.Instance.PlaySound("OnTutorialShowUp");

        CreateDialogue();
        Debug.Log("Interacting with NPC class");
    }

    private void CreateDialogue()
    {
        LoadDialogueFile();

        DialogueSystem.Instance.AddNewDialogue(dialogue, this._name);

    }

    public override void DisableInteraction()
    {
        IsInDialogue = false;
        DialogueSystem.Instance.HideDialogue();
    }
}
