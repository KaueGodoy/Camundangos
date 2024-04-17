using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;

    private int _currentCharacterIndex = 0;
    private Vector3 _savedPosition;

    private PlayerInputHandler _inputHandler;
    private AudioManager _audioManager;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        EnableFirstCharacter();
    }

    private void EnableFirstCharacter()
    {
        foreach (var character in _characters)
        {
            if (character != _characters[0])
                character.SetActive(false);
        }
    }

    private void Update()
    {
        if (_inputHandler.HasCurrentCharacterChangedToOne())
        {
            SwitchCharacter(0);
        }
        if (_inputHandler.HasCurrentCharacterChangedToTwo())
        {
            SwitchCharacter(1);
        }
        if (_inputHandler.HasCurrentCharacterChangedToThree())
        {
            SwitchCharacter(2);
        }
        if (_inputHandler.HasCurrentCharacterChangedToFour())
        {
            SwitchCharacter(3);
        }
    }

    private void SwitchCharacter(int newIndex)
    {
        if (newIndex >= 0 && newIndex < _characters.Length)
        {
            _characters[_currentCharacterIndex].SetActive(false);
            _savedPosition = _characters[_currentCharacterIndex].transform.position;

            _characters[newIndex].SetActive(true);
            _characters[newIndex].transform.position = _savedPosition;

            _currentCharacterIndex = newIndex;

            _audioManager.PlaySound("OnCharacterSwitch");
        }
        else if (newIndex >= _characters.Length)
        {
            Debug.LogWarning("Character locked!");
        }
    }
}
