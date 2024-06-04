using System;
using UnityEngine;

public class ChangeCharacterController : MonoBehaviour
{
    public static ChangeCharacterController Instance { get; private set; }     

    public event EventHandler OnCharacterChangedFailed;

    public event EventHandler OnCharacterChangedParticles;

    [SerializeField] private GameObject _derildoVisual;

    [Header("Characters")]
    [SerializeField] private GameObject _marceloVisual;
    [SerializeField] private GameObject _matiasVisual;
    [SerializeField] private GameObject _isaVisual;
    [SerializeField] private GameObject _leoVisual;

    [SerializeField] private GameObject _currentCharacter;

    private string _onCharacterChangedSFX = "OnCharacterSwitch";

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        _currentCharacter = _marceloVisual;
        _currentCharacter.SetActive(true);

        GameInput.Instance.OnCharacterChanged_Slot01 += GameInput_OnCharacterChanged_Slot01;
        GameInput.Instance.OnCharacterChanged_Slot02 += GameInput_OnCharacterChanged_Slot02;
        GameInput.Instance.OnCharacterChanged_Slot03 += GameInput_OnCharacterChanged_Slot03;
        GameInput.Instance.OnCharacterChanged_Slot04 += GameInput_OnCharacterChanged_Slot04;
    }

    private void GameInput_OnCharacterChanged_Slot04(object sender, System.EventArgs e)
    {
        if (NewPlayerMovement.Instance.IsControlLocked) return;

        if (OnCharacterUnlocked.Instance.IsLeoUnlocked)
            UpdateCurrentCharacter(GetCurrentCharacter(), _leoVisual);
        else
            OnCharacterChangedFailed?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnCharacterChanged_Slot03(object sender, System.EventArgs e)
    {
        if (NewPlayerMovement.Instance.IsControlLocked) return;

        if (OnCharacterUnlocked.Instance.IsIsaUnlocked)
            UpdateCurrentCharacter(GetCurrentCharacter(), _isaVisual);
        else
            OnCharacterChangedFailed?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnCharacterChanged_Slot02(object sender, System.EventArgs e)
    {
        if (NewPlayerMovement.Instance.IsControlLocked) return;

        if (OnCharacterUnlocked.Instance.IsMatiasUnlocked)
            UpdateCurrentCharacter(GetCurrentCharacter(), _matiasVisual);
        else
            OnCharacterChangedFailed?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnCharacterChanged_Slot01(object sender, System.EventArgs e)
    {
        if (NewPlayerMovement.Instance.IsControlLocked) return;

        UpdateCurrentCharacter(GetCurrentCharacter(), _marceloVisual);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    UpdateCurrentCharacter(GetCurrentCharacter(), _derildoVisual);
        //}
    }

    private void UpdateCurrentCharacter(GameObject previousCharacter, GameObject newCharacter)
    {
        if (previousCharacter == newCharacter) return;

        previousCharacter.SetActive(false);
        _currentCharacter = newCharacter;
        _currentCharacter.SetActive(true);

        OnCharacterChangedParticles?.Invoke(this, EventArgs.Empty);
        AudioManager.Instance.PlaySound(_onCharacterChangedSFX);
    }

    private GameObject GetCurrentCharacter()
    {
        return _currentCharacter;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnCharacterChanged_Slot01 -= GameInput_OnCharacterChanged_Slot01;
        GameInput.Instance.OnCharacterChanged_Slot02 -= GameInput_OnCharacterChanged_Slot02;
        GameInput.Instance.OnCharacterChanged_Slot03 -= GameInput_OnCharacterChanged_Slot03;
        GameInput.Instance.OnCharacterChanged_Slot04 -= GameInput_OnCharacterChanged_Slot04;
    }
}
