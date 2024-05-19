using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCreditsScene : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {
            GoBackToMainMenu();
        }

    }

    public void GoBackToMainMenu()
    {
        _fader.gameObject.SetActive(true);

        Time.timeScale = 1f;

        LeanTween.scale(_fader, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEase(LeanTweenType.easeInExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(2);
        });

    }
}
