using UnityEngine;

public class CharacterPanelUI : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private RectTransform _characterPanel;

    public bool PanelIsActive { get; set; }

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        _characterPanel.gameObject.SetActive(false);

        GameInput.Instance.OnCharacterStatsPressed += GameInput_OnCharacterStatsPressed;
    }

    private void GameInput_OnCharacterStatsPressed(object sender, System.EventArgs e)
    {
        ActivateMenu();
    }

    private void ActivateMenu()
    {
        PanelIsActive = !PanelIsActive;
        _characterPanel.gameObject.SetActive(PanelIsActive);
        OpenMenu(PanelIsActive);
        CloseMenu(!PanelIsActive);
    }

    private bool OpenMenu(bool menuOpened)
    {
        if (menuOpened)
            _audioManager.PlaySound("OnStatsOpened");

        return menuOpened;
    }

    private bool CloseMenu(bool menuClosed)
    {
        if (menuClosed)
            _audioManager.PlaySound("OnStatsClosed");

        return menuClosed;
    }
}
