using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyPanelUI : MonoBehaviour
{
    [SerializeField] private Image _characterIcon0;
    [SerializeField] private Image _characterIcon1;
    [SerializeField] private Image _characterIcon2;
    [SerializeField] private Image _characterIcon3;

    private void Start()
    {
        //_characterIcon1.sprite = 

        OnCharacterUnlocked.Instance.OnIsaUnlocked += OnCharacterUnlocked_OnIsaUnlocked;
    }

    private void OnCharacterUnlocked_OnIsaUnlocked(object sender, System.EventArgs e)
    {
        UpdateCharacterIcon();
    }

    private void UpdateCharacterIcon()
    {
        //_characterIcon1.sprite = 
    }
}
