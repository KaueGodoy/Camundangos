using UnityEngine;

public class OnCollisionShowTutorial : MonoBehaviour
{
    private GameObject _tutorialText;

    private void Start()
    {
        _tutorialText = transform.Find("TutorialText").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_tutorialText != null)
            {
                AudioManager.Instance.PlaySound("OnTutorialShowUp");
                _tutorialText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (_tutorialText != null)
            {
                _tutorialText.SetActive(false);
            }
        }
    }
}
