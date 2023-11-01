using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;

    private int _currentCharacterIndex = 0;
    private Vector3 _savedPosition;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCharacter(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCharacter(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCharacter(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCharacter(3);
        }
    }

    void SwitchCharacter(int newIndex)
    {
        if (newIndex >= 0 && newIndex < _characters.Length)
        {
            _characters[_currentCharacterIndex].SetActive(false);
            _savedPosition = _characters[_currentCharacterIndex].transform.position;

            _characters[newIndex].SetActive(true);
            _characters[newIndex].transform.position = _savedPosition;

            _currentCharacterIndex = newIndex;
        }
    }
}
