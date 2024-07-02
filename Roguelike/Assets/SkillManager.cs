using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [Header("Character Skills")]
    [SerializeField] private GameObject _marceloSkill;
    [SerializeField] private GameObject _matiasSkill;
    [SerializeField] private GameObject _isaSkill;
    [SerializeField] private GameObject _leoSkill;

    private GameObject _currentSkill;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCurrentSkill(GameObject currentCharacter)
    {
        if (currentCharacter.name == "MarceloVisual")
        {
            _currentSkill = _marceloSkill;
        }
        else if (currentCharacter.name == "MatiasVisual")
        {
            _currentSkill = _matiasSkill;
        }
        else if (currentCharacter.name == "IsaVisual")
        {
            _currentSkill = _isaSkill;
        }
        else if (currentCharacter.name == "LeoVisual")
        {
            _currentSkill = _leoSkill;
        }

        PlayerSkill.Instance.SetCurrentSkill(_currentSkill);
    }
}
