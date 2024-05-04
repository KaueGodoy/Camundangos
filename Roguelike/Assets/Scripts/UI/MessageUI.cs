using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageHideDelay = 0.5f;

    //  Turn this string into a txt file and load it using an event, instead of populating the dialogue on validade as we are doing rn 
    //  This way we can make sure the dialogue is being loaded correctly even using a file
    //  Becasue rn it works loading a string

    private string _onCharacterChangeFailedMessage_EnUs = "Continue <color=yellow>exploring </color>to unlock this <color=green>character </color>";
    private string _onCharacterChangeFailedMessage_PtBr = "Continue <color=yellow>explorando </color>para desbloquear este <color=green>personagem </color>";
    private string _onCharacterChangeFailedMessage_Jp = "この<color=green>キャラ </color>をアンロックするには<color=yellow>探索 </color>を続けて";

    private string _onCharacterChangeFailedSFX = "OnCharacterChangedFailed";

    [SerializeField] private Animator _animator;
    private string _fadeInAnimation = "FadeInAnimation";

    private int _englishLocaleIndex = 0;
    private int _brazilianPortugueseLocaleIndex = 1;
    private int _JapaneseLocaleIndex = 2;

    private string _onCharacterChangeFailedMessageKey = "PlayKey";

    private void Start()
    {
        ChangeCharacterController.Instance.OnCharacterChangedFailed += ChangeCharacterController_OnCharacterChangedFailed;

        Hide();
    }

    private void ChangeCharacterController_OnCharacterChangedFailed(object sender, System.EventArgs e)
    {

        LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_onCharacterChangeFailedMessageKey).Completed += handle =>
        {
            string localizedMessage = handle.Result;

            ShowMessage(localizedMessage, _onCharacterChangeFailedSFX);
        };

        //if (LocalizationManager.Instance.IsLanguageEnglish())
        //{

        //    ShowMessage(_onCharacterChangeFailedMessage_EnUs, _onCharacterChangeFailedSFX);
        //}
        //else if (LocalizationManager.Instance.IsLanguageBrazilianPortuguese())
        //{
        //    ShowMessage(_onCharacterChangeFailedMessage_PtBr, _onCharacterChangeFailedSFX);
        //}
        //else if (LocalizationManager.Instance.IsLanguageJapanese())
        //{
        //    ShowMessage(_onCharacterChangeFailedMessage_Jp, _onCharacterChangeFailedSFX);
        //}
    }

    private void ShowMessage(string message, string audioString)
    {
        Show();
        _messageText.text = message;
        _animator.Play(_fadeInAnimation);
        AudioManager.Instance.PlaySoundOneShot(audioString);

        StartCoroutine(HideAfterDelay(_messageHideDelay));
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ChangeCharacterController.Instance.OnCharacterChangedFailed -= ChangeCharacterController_OnCharacterChangedFailed;
    }
}
