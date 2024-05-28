using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class OnCollisionShowTutorial : MonoBehaviour
{
    [SerializeField] private string _tutorialKey = " ";

    private GameObject _tutorialGameObject;
    private TextMeshProUGUI _tutorialText;

    private void Start()
    {
        _tutorialGameObject = transform.Find("TutorialText").gameObject;
        _tutorialText = _tutorialGameObject.GetComponentInChildren<TextMeshProUGUI>();

        PopulateText();
    }

    private void PopulateText()
    {
        LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_tutorialKey).Completed += handle =>
        {
            string localizedMessage = handle.Result;

            ShowMessage(localizedMessage, "OnTutorialShowUp");
        };
    }

    private void ShowMessage(string message, string audioString)
    {
        _tutorialText.text = message;
        AudioManager.Instance.PlaySound(audioString);
        //Debug.Log("Message showing");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_tutorialGameObject != null)
            {
                _tutorialGameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_tutorialGameObject != null)
            {
                _tutorialGameObject.SetActive(false);
            }
        }
    }
}
