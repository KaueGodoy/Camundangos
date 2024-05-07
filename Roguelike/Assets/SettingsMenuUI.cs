using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Button _languageButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _languagePanel;

    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _audioCloseButton;
    [SerializeField] private Transform _audioPanel;

    private void Awake()
    {
        _languageButton.onClick.AddListener(() =>
        {
            //_languagePanel.gameObject.SetActive(true);
            ShowAndHide(_languagePanel);
        });
        _closeButton.onClick.AddListener(() =>
        {
            ShowAndHide(_languagePanel);
        }
        );

        _audioButton.onClick.AddListener(() =>
        {
            ShowAndHide(_audioPanel);
        }
        );
        _audioCloseButton.onClick.AddListener(() =>
        {
            ShowAndHide(_audioPanel);
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

}
