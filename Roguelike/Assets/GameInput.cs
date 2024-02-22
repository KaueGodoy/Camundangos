using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPlayerJump;

    private PlayerControls _playerControls;

    private void Awake()
    {
        Instance = this;

        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();

        _playerControls.Player.Jump.performed += Jump_performed;
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
    }

}
