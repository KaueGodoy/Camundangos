using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGameScene : MonoBehaviour
{
    [SerializeField] private GameObject _fader;

    private int _gameSceneIndex = 2;

    public void GoToGameSceneOnAnimationFinished()
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
