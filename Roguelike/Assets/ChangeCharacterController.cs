using UnityEngine;

public class ChangeCharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _derildoVisual;

    [Header("Characters")]
    [SerializeField] private GameObject _marceloVisual;
    [SerializeField] private GameObject _matiasVisual;
    [SerializeField] private GameObject _isaVisual;
    [SerializeField] private GameObject _leoVisual;

    [SerializeField] private GameObject _currentCharacter;

    private void Start()
    {
        _currentCharacter = _derildoVisual;
        _currentCharacter.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCurrentCharacter(GetCurrentCharacter(), _marceloVisual);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateCurrentCharacter(GetCurrentCharacter(), _matiasVisual);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateCurrentCharacter(GetCurrentCharacter(), _isaVisual);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateCurrentCharacter(GetCurrentCharacter(), _leoVisual);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateCurrentCharacter(GetCurrentCharacter(), _derildoVisual);
        }
    }

    private void UpdateCurrentCharacter(GameObject previousCharacter, GameObject newCharacter)
    {
        previousCharacter.SetActive(false);
        _currentCharacter = newCharacter;
        _currentCharacter.SetActive(true);
    }

    private GameObject GetCurrentCharacter()
    {
        return _currentCharacter;
    }
}
