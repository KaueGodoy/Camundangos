using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerDash;

    public event EventHandler OnCharacterChanged_Slot01;
    public event EventHandler OnCharacterChanged_Slot02;
    public event EventHandler OnCharacterChanged_Slot03;
    public event EventHandler OnCharacterChanged_Slot04;

    public event EventHandler OnInventoryPressed;
    public event EventHandler OnCharacterStatsPressed;
    public event EventHandler OnPausePressed;

    private PlayerControls _playerControls;

    private void Awake()
    {
        Instance = this;

        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();

        _playerControls.Player.Jump.performed += Jump_performed;
        _playerControls.Player.Dash.performed += Dash_performed;

        _playerControls.UI.Enable();
        _playerControls.UI.Character1.performed += Character1_performed;
        _playerControls.UI.Character2.performed += Character2_performed;
        _playerControls.UI.Character3.performed += Character3_performed;
        _playerControls.UI.Character4.performed += Character4_performed;

        _playerControls.UI.Inventory.performed += Inventory_performed;
        _playerControls.UI.Stats.performed += Stats_performed;
        _playerControls.UI.Pause.performed += Pause_performed;

    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPausePressed?.Invoke(this, EventArgs.Empty);
    }

    private void Stats_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCharacterStatsPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInventoryPressed?.Invoke(this, EventArgs.Empty);  
    }

    private void Character4_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCharacterChanged_Slot04?.Invoke(this, EventArgs.Empty);
    }

    private void Character3_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCharacterChanged_Slot03?.Invoke(this, EventArgs.Empty);
    }

    private void Character2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCharacterChanged_Slot02?.Invoke(this, EventArgs.Empty);
    }

    private void Character1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCharacterChanged_Slot01?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void OnDestroy()
    {
        _playerControls.Player.Jump.performed -= Jump_performed;
        _playerControls.Player.Dash.performed -= Dash_performed;

        _playerControls.UI.Character1.performed -= Character1_performed;
        _playerControls.UI.Character2.performed -= Character2_performed;
        _playerControls.UI.Character3.performed -= Character3_performed;
        _playerControls.UI.Character4.performed -= Character4_performed;

        _playerControls.UI.Inventory.performed -= Inventory_performed;
        _playerControls.UI.Stats.performed -= Stats_performed;
        _playerControls.UI.Pause.performed -= Pause_performed;


    }

}
