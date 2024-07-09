using UnityEngine;

public class UltManager : MonoBehaviour
{
    public static UltManager Instance { get; private set; }

    [Header("Character Ults")]
    [SerializeField] private GameObject _marceloUlt;
    [SerializeField] private GameObject _matiasUlt;
    [SerializeField] private GameObject _isaUlt;
    [SerializeField] private GameObject _leoUlt;

    private GameObject _currentUlt;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCurrentUlt(GameObject currentCharacter)
    {
        if (currentCharacter.name == "MarceloVisual")
        {
            _currentUlt = _marceloUlt;
        }
        else if (currentCharacter.name == "MatiasVisual")
        {
            _currentUlt = _matiasUlt;
        }
        else if (currentCharacter.name == "IsaVisual")
        {
            _currentUlt = _isaUlt;
        }
        else if (currentCharacter.name == "LeoVisual")
        {
            _currentUlt = _leoUlt;
        }

        PlayerUlt.Instance.SetCurrentUlt(_currentUlt);
    }
}
