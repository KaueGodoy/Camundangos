using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Button _languageButton;
    [SerializeField] private Transform _languagePanel;

    private void Awake()
    {
        _languageButton.onClick.AddListener(() =>
        {
            //_languagePanel.gameObject.SetActive(true);
            ShowAndHide(_languagePanel);
        });
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
    }

}
