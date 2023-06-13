using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    public void PlayGame()
    {
        // Alpha
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0f);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
