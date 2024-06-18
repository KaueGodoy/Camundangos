using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    
    private int _englishLocaleIndex = 0;
    private int _brazilianPortugueseLocaleIndex = 1;
    private int _japaneseLocaleIndex = 2;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsLanguageEnglish()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_englishLocaleIndex])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsLanguageBrazilianPortuguese()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_brazilianPortugueseLocaleIndex])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsLanguageJapanese()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[_japaneseLocaleIndex])
        {
            return true;
        }
        else
        {
            return false;
        }
    }




}
