using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;


    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            Play();
        });

        _settingsButton.onClick.AddListener(() =>
        {

        });

        _quitButton.onClick.AddListener(() =>
        {
            Quit();
        });
    }

    public void Play()
    {
        // Alpha
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
        Application.Quit();
    }
}
