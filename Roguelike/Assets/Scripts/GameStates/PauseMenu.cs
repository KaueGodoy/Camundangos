using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private RectTransform _fader;

    private void Awake()
    {
        GameIsPaused = false;
    }

    private void Start()
    {
        SetFader();

        GameInput.Instance.OnPausePressed += GameInput_OnPausePressed;
    }

    private void GameInput_OnPausePressed(object sender, System.EventArgs e)
    {
        HandlePauseState();
    }

    private void HandlePauseState()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void SetFader()
    {
        _fader.gameObject.SetActive(false);
        LeanTween.scale(_fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(_fader, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            _fader.gameObject.SetActive(false);
        });
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // disable all audio
        //AudioListener.pause = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        _fader.gameObject.SetActive(true);

        //LeanTween.scale(fader, Vector3.zero, 0f);
        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setOnComplete(() =>
        //{
        //    SceneManager.LoadScene(0);
        //});

        _pauseMenuUI.gameObject.SetActive(false);

        LeanTween.scale(_fader, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEase(LeanTweenType.easeInExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });

        Time.timeScale = 1f;

        //Invoke("TransitionToMainMenu", 1f);

    }
    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

}
