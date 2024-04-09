using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyPanelUI : MonoBehaviour
{
    [SerializeField] private Image _matiasLockedIcon;
    [SerializeField] private Image _isaLockedIcon;
    [SerializeField] private Image _leoLockedICon;

    private string _pathToUnlockedSprite = "UI/Icons/CharacterUnlocked/";

    private Sprite _matiasUnlockedSprite;
    private Sprite _isaUnlockedSprite;
    private Sprite _leoUnlockedSprite;

    private void Start()
    {
        _isaUnlockedSprite = Resources.Load<Sprite>(_pathToUnlockedSprite + "isa_ratinha_icone");
        _matiasUnlockedSprite = Resources.Load<Sprite>(_pathToUnlockedSprite + "matias_mus_icone");
        _leoUnlockedSprite = Resources.Load<Sprite>(_pathToUnlockedSprite + "leo_chumbinho_icone");

        OnCharacterUnlocked.Instance.OnIsaUnlocked += OnCharacterUnlocked_OnIsaUnlocked;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked += OnCharacterUnlocked_OnMatiasUnlocked;
        OnCharacterUnlocked.Instance.OnLeoUnlocked += OnCharacterUnlocked_OnLeoUnlocked;
    }

    private void OnCharacterUnlocked_OnLeoUnlocked(object sender, System.EventArgs e)
    {
        UpdateCharacterIcon(_leoLockedICon, _leoUnlockedSprite);
    }

    private void OnCharacterUnlocked_OnMatiasUnlocked(object sender, System.EventArgs e)
    {
        UpdateCharacterIcon(_matiasLockedIcon, _matiasUnlockedSprite);
    }

    private void OnCharacterUnlocked_OnIsaUnlocked(object sender, System.EventArgs e)
    {
        UpdateCharacterIcon(_isaLockedIcon, _isaUnlockedSprite);
    }

    private void UpdateCharacterIcon(Image lockedCharacterIcon, Sprite unlockCharacterIcon)
    {
        lockedCharacterIcon.sprite = unlockCharacterIcon;
    }

    private void OnDestroy()
    {
        OnCharacterUnlocked.Instance.OnIsaUnlocked -= OnCharacterUnlocked_OnIsaUnlocked;
        OnCharacterUnlocked.Instance.OnMatiasUnlocked -= OnCharacterUnlocked_OnMatiasUnlocked;
        OnCharacterUnlocked.Instance.OnLeoUnlocked -= OnCharacterUnlocked_OnLeoUnlocked;
    }
}
