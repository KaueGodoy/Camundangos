using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    //public event EventHandler OnInteractAction;

    private PlayerControls _playerControls;

    private void Awake()
    {
        Instance = this;

        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();

        //_playerInputActions.Player.Interact.performed += Interact_performed;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void OnDestroy()
    {
        //_playerInputActions.Player.Interact.performed -= Interact_performed;
    }

}
