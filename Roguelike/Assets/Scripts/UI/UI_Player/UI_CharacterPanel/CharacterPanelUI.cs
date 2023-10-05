using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPanelUI : MonoBehaviour
{
    public RectTransform characterPanel;
    bool PanelIsActive { get; set; }

    private PlayerControls playerControls;

    private AudioManager _audioManager;


    private void Awake()
    {
        playerControls = new PlayerControls();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        characterPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerControls.UI.Stats.triggered)
        {
            ActivateMenu();
        }
    }

    private void ActivateMenu()
    {
        PanelIsActive = !PanelIsActive;
        characterPanel.gameObject.SetActive(PanelIsActive);
        OpenMenu(PanelIsActive);
    }

    private bool OpenMenu(bool menuOpened)
    {
        if (menuOpened)
            _audioManager.PlaySound("OnStatsOpened");

        return menuOpened;
    }
}
