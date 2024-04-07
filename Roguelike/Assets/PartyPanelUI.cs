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

    private string _pathToUnlockedSprite = "UI/Icons/CharacterUnlocked/";
    private Sprite _isaUnlockedSprite;

    private void Start()
    {
        _isaUnlockedSprite = Resources.Load<Sprite>(_pathToUnlockedSprite + "isa_ratinha_icone");

        OnCharacterUnlocked.Instance.OnIsaUnlocked += OnCharacterUnlocked_OnIsaUnlocked;
    }

    private void OnCharacterUnlocked_OnIsaUnlocked(object sender, System.EventArgs e)
    {
        UpdateIsaCharacterIcon();
    }

    private void UpdateIsaCharacterIcon()
    {
        _characterIcon2.sprite = _isaUnlockedSprite;
    }
}
