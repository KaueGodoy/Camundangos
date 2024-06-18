using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    [SerializeField] private Button _englishLanguageButton;
    [SerializeField] private Button _brazilianPortugueseLanguageButton;
    [SerializeField] private Button _japaneseLanguageButton;
    [SerializeField] private Button _spanishLanguageButton;

    private bool _isActive = false;

    private string _localeKey = "LocaleKey";
    private int _currentLocaleId = 0;

    private int _englishLocaleId = 0;
    private int _brazilianPortugueseLocaleId = 1;
    private int _japaneseLocaleId = 2;
    private int _spanishLocaleId = 3;

    private void Awake()
    {
        Instance = this;

        _englishLanguageButton.onClick.AddListener(() =>
        {
            ChangeLocaleId(_englishLocaleId);
        });

        _brazilianPortugueseLanguageButton.onClick.AddListener(() =>
        {
            ChangeLocaleId(_brazilianPortugueseLocaleId);
        });

        _japaneseLanguageButton.onClick.AddListener(() =>
        {
            ChangeLocaleId(_japaneseLocaleId);
        });

        _spanishLanguageButton.onClick.AddListener(() =>
        {
            ChangeLocaleId(_spanishLocaleId);
        });

    }

    private void Start()
    {
        int id = PlayerPrefs.GetInt(_localeKey, _currentLocaleId);
        //ChangeLocaleId(id);

        if (_isActive) return;
        CallSetLocaleCoroutine(id);
    }

    public void ChangeLocaleId(int localeId)
    {
        if (_isActive) return;

        AudioManager.Instance.PlaySound("OnUIPressed");
        CallSetLocaleCoroutine(localeId);
        Debug.Log("Locale id: " + _currentLocaleId);
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
