using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageShowDelay = 0.5f;
    [SerializeField] private float _messageHideDelay = 0.5f;

    private string _onQuestUpdateSFX = "OnQuestUpdateSFX";
    //private string _currentQuest = "Encontre Isa na floresta";

    [SerializeField] private Animator _animator;

    private string _fadeInAnimation = "FadeInAnimation";

    private void Start()
    {
        //Hide();
        _animator.Play(_fadeInAnimation);
        
        StartCoroutine(ShowAfterDelay(_messageShowDelay));
    }

    private void ShowMessage(string message, string audioString)
    {
        StartCoroutine(HideAfterDelay(_messageHideDelay));

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

    private IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ShowMessage(_messageText.text, _onQuestUpdateSFX);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
