using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsSceneUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private RectTransform _fader;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            GoBackToMainMenu();
        });
    }

    private void Start()
    {
        _mainMenuButton.Select();
    }

    public void GoBackToMainMenu()
    {
        PauseMenu.GameIsPaused = false;

        //_fader.gameObject.SetActive(true);

        //LeanTween.scale(_fader, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEase(LeanTweenType.easeInExpo).setOnComplete(() =>
        //{
        //    SceneManager.LoadScene(0);
        //});

        Time.timeScale = 1f;

        // Alpha
        AudioManager.Instance.PlaySound("OnUIPressed");

        _fader.gameObject.SetActive(true);
        LeanTween.alpha(_fader, 0, 0f);
        LeanTween.alpha(_fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
