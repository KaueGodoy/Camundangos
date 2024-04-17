using UnityEngine;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Move.performed += context => HandleMoveInput(context.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Move.performed -= context => HandleMoveInput(context.ReadValue<Vector2>());
    }

    private void HandleMoveInput(Vector2 moveInput)
    {
        OnMoveInput?.Invoke(moveInput);
    }

    public Vector2 GetMovementInput()
    {
        return _playerControls.Player.Move.ReadValue<Vector2>();
    }

    public bool IsJumpPressed()
    {
        return _playerControls.Player.Jump.triggered;
    }

    public bool IsAttackPressed()
    {
        return _playerControls.Player.Attack.triggered;
    }

    public bool IsSkillPressed()
    {
        return _playerControls.Player.Skill.triggered;
    }

    public bool IsUltPressed()
    {
        return _playerControls.Player.Ult.triggered;
    }

    public bool HasCurrentCharacterChangedToOne()
    {
        return _playerControls.UI.Character1.triggered;
    }

    public bool HasCurrentCharacterChangedToTwo()
    {
        return _playerControls.UI.Character2.triggered;
    }

    public bool HasCurrentCharacterChangedToThree()
    {
        return _playerControls.UI.Character3.triggered;
    }

    public bool HasCurrentCharacterChangedToFour()
    {
        return _playerControls.UI.Character4.triggered;
    }

}
