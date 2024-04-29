using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    private bool _isActive = false;

    private string _localeKey = "LocaleKey";
    private int _defaultLocaleId = 0;

    private void Start()
    {
        int id = PlayerPrefs.GetInt(_localeKey, _defaultLocaleId);
        ChangeLocaleId(id);
    }

    public void ChangeLocaleId(int localeId)
    {
        if (_isActive) return;

        CallSetLocaleCoroutine(localeId);
    }

    private void CallSetLocaleCoroutine(int localeId)
    {
        StartCoroutine(SetLocale(localeId));
    }

    private IEnumerator SetLocale(int localeId)
    {
        _isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        PlayerPrefs.SetInt(_localeKey, localeId);
        _isActive = false;
    }


}
