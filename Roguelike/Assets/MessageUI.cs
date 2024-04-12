using System.Collections;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageHideDelay = 0.5f;

    private string _onCharacterChangeFailedMessage = "Continue <color=yellow>explorando </color>para desbloquear este <color=green>personagem </color>";
    private string _onCharacterChangeFailedSFX = "OnCharacterChangedFailed";

    private void Start()
    {
        ChangeCharacterController.Instance.OnCharacterChangedFailed += ChangeCharacterController_OnCharacterChangedFailed;

        Hide();
    }

    private void ChangeCharacterController_OnCharacterChangedFailed(object sender, System.EventArgs e)
    {
        ShowMessage(_onCharacterChangeFailedMessage, _onCharacterChangeFailedSFX);
    }

    private void ShowMessage(string message, string audioString)
    {
        Show();
        _messageText.text = message;
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
