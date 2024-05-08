using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Transform _settingsPanel;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            Play();
        });

        _settingsButton.onClick.AddListener(() =>
        {
            OpenSettings();
        });

        _quitButton.onClick.AddListener(() =>
        {
            Quit();
        });
    }

    private void OpenSettings()
    {
        AudioManager.Instance.PlaySound("OnUIPressed");

        this.gameObject.SetActive(false);
        _settingsPanel.gameObject.SetActive(true);
    }

    public void Play()
    {
        // Alpha
        AudioManager.Instance.PlaySound("OnUIPressed");

        _fader.gameObject.SetActive(true);
        LeanTween.alpha(_fader, 0, 0f);
        LeanTween.alpha(_fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        AudioManager.Instance.PlaySound("OnUIPressed");

        Application.Quit();
    }
}
