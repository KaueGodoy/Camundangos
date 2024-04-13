using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelVisual : MonoBehaviour
{
    private Animator _animator;
    private string _fadeInAnimation = "DialogueFadeInAnimation";
    private string _fadeOutAnimation = "DialogueFadeOutAnimation";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnDialogueCreatedPlayAnimation()
    {
        _animator.Play(_fadeInAnimation);
    }

    public void OnDialogueFinishedPlayAnimation()
    {
        _animator.Play(_fadeOutAnimation);
    }

    private IEnumerator LerpImageToAlpha(float targetAlpha, float duration, Image image)
    {
        Color currentColor = image.color;
        float startAlpha = currentColor.a;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        if (image != null)
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

    public void CallLerpImageToAlphaCoroutine(float targetAlpha, float duration, Image image)
    {
        StartCoroutine(LerpImageToAlpha(targetAlpha, duration, image));
    }

}
