using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPanelUI : MonoBehaviour
{
    public RectTransform characterPanel;
    bool PanelIsActive { get; set; }

    public PlayerInput playerInput;

    void Start()
    {
        characterPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || playerInput.actions["Stats"].triggered)
        {
            PanelIsActive = !PanelIsActive;
            characterPanel.gameObject.SetActive(PanelIsActive);
        }
    }
}
