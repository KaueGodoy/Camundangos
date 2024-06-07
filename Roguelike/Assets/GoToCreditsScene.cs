using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCreditsScene : MonoBehaviour
{
    [SerializeField] private RectTransform _goodEnd;
    [SerializeField] private RectTransform _badEnd;
    [SerializeField] private RectTransform _fader;

    private int _creditsSceneIndex = 2;
    private bool _hasCollided = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {
            if (!_hasCollided)
            {
                _hasCollided = true;
                GoBackToMainMenu();
            }
        }

    }

    public void GoBackToMainMenu()
    {
        _fader.gameObject.SetActive(true);

        Time.timeScale = 1f;

        LeanTween.scale(_fader, new Vector3(1f, 1f, 1), 1.5f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
        {
            LeanTween.scale(_fader, new Vector3(1.4f, 1.4f, 1.4f), 5f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
            {
                SceneManager.LoadScene(_creditsSceneIndex);
            });
        });

    }
}
