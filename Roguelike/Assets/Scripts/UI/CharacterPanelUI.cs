using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPanelUI : MonoBehaviour
{
    public RectTransform characterPanel;
    bool PanelIsActive { get; set; }

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
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
            PanelIsActive = !PanelIsActive;
            characterPanel.gameObject.SetActive(PanelIsActive);
        }
    }
}
