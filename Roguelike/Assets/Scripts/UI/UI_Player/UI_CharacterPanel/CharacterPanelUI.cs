using UnityEngine;

public class CharacterPanelUI : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private RectTransform _characterPanel;

    public bool PanelIsActive { get; set; }

    void Start()
    {
        _characterPanel.gameObject.SetActive(false);

        GameInput.Instance.OnCharacterStatsPressed += GameInput_OnCharacterStatsPressed;
    }

    private void GameInput_OnCharacterStatsPressed(object sender, System.EventArgs e)
    {
        ActivateMenu();

        MouseManager.Instance.TriggerCursorState();
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
            AudioManager.Instance.PlaySound("OnStatsOpened");

        return menuOpened;
    }

    private bool CloseMenu(bool menuClosed)
    {
        if (menuClosed)
            AudioManager.Instance.PlaySound("OnStatsClosed");

        return menuClosed;
    }
}
