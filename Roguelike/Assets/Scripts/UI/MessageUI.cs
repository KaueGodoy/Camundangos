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

    [SerializeField] private Animator _animator;
    private string _fadeInAnimation = "FadeInAnimation";

    private string _onCharacterChangeFailedMessageKey = "UnlockCharacterKey";
    private string _onBossEntranceFailedMessageKey = "UnlockCharacterKey";

    private string _onCharacterChangeFailedSFX = "OnCharacterChangedFailed";

    private void Start()
    {
        ChangeCharacterController.Instance.OnCharacterChangedFailed += ChangeCharacterController_OnCharacterChangedFailed;
        BossEntrance.Instance.OnBossEntranceFailed += BossEntrance_OnBossEntranceFailed;

        Hide();
    }

    private void BossEntrance_OnBossEntranceFailed(object sender, EventArgs e)
    {
        LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_onBossEntranceFailedMessageKey).Completed += handle =>
        {
            string localizedMessage = handle.Result;

            ShowMessage(localizedMessage, _onCharacterChangeFailedSFX);
        };
    }

    private void ChangeCharacterController_OnCharacterChangedFailed(object sender, System.EventArgs e)
    {

        LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_onCharacterChangeFailedMessageKey).Completed += handle =>
        {
            string localizedMessage = handle.Result;

            ShowMessage(localizedMessage, _onCharacterChangeFailedSFX);
        };
    }

    private void ShowMessage(string message, string audioString)
    {
        Show();
        _messageText.text = message;
        _animator.Play(_fadeInAnimation);
        AudioManager.Instance.PlaySound(audioString);

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
