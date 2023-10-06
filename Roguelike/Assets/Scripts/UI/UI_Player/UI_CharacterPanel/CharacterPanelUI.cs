using UnityEngine;

public class CharacterPanelUI : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private RectTransform _characterPanel;

    public bool PanelIsActive { get; set; }

    private PlayerControls _playerControls;
    private AudioManager _audioManager;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        _characterPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Update()
    {
        if (_playerControls.UI.Stats.triggered)
        {
            ActivateMenu();
        }
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
