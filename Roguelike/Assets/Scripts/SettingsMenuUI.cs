using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Transform _languagePanel;
    [SerializeField] private Button _languageButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _englishLanguageButton;

    [SerializeField] private Transform _audioPanel;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _audioCloseButton;
    [SerializeField] private Slider _masterAudioSlider;

    private void Awake()
    {
        _languageButton.onClick.AddListener(() =>
        {
            ShowAndHideWithSelectableButton(_languagePanel, _englishLanguageButton);
        });
        _closeButton.onClick.AddListener(() =>
        {
            ShowAndHide(_languagePanel);
            _languageButton.Select();
        }
        );

        _audioButton.onClick.AddListener(() =>
        {
            ShowAndHideWithSelectableSlider(_audioPanel, _masterAudioSlider);
        }
        );
        _audioCloseButton.onClick.AddListener(() =>
        {
            ShowAndHide(_audioPanel);
            _languageButton.Select();
        }
        );
    }

    private void ShowAndHide(Transform obj)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj.gameObject.SetActive(true);
        }

        AudioManager.Instance.PlaySound("OnUIPressed");
    }

    private void ShowAndHideWithSelectableButton(Transform obj, Button button)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj.gameObject.SetActive(true);
            button.Select();
        }

        AudioManager.Instance.PlaySound("OnUIPressed");
    }

    private void ShowAndHideWithSelectableSlider(Transform obj, Slider slider)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj.gameObject.SetActive(true);
            slider.Select();
        }

        AudioManager.Instance.PlaySound("OnUIPressed");
    }

}
