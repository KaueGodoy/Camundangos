using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelUI : MonoBehaviour
{
    public RectTransform characterPanel;
    bool PanelIsActive { get; set; }

    void Start()
    {
        characterPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PanelIsActive = !PanelIsActive;
            characterPanel.gameObject.SetActive(PanelIsActive);
        }
    }
}
