using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelUI : MonoBehaviour
{
    [SerializeField] private Transform _menuPanel;
    [SerializeField] private Button _returnButton;

    private void Awake()
    {
        _returnButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            _menuPanel.gameObject.SetActive(true);
            AudioManager.Instance.PlaySound("OnUIPressed");
        });
    }
}
