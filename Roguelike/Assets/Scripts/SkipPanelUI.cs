using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipPanelUI : MonoBehaviour
{
    [SerializeField] private Button _skipButton;
    [SerializeField] private RectTransform _fader;

    private int _gameSceneIndex = 2;

    private void Awake()
    {
        _skipButton.onClick.AddListener(() =>
        {
            GoToGameScene();
        });
    }

    private void GoToGameScene()
    {
        AudioManager.Instance.PlaySound("OnUIPressed");

        _fader.gameObject.SetActive(true);
        LeanTween.alpha(_fader, 0, 0f);
        LeanTween.alpha(_fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(_gameSceneIndex);
        });
    }
}
