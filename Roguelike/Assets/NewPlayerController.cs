using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;

    [Header("Mobile")]
    [SerializeField] private GameObject _mobileUI;
    private bool isMobileUIActive;

    private void Start()
    {
        _playerHealth.UpdatePlayerHealthBar();
    }

    private void Update()
    {
        if (!PlayerHealth.IsAlive) return;

        _playerAttack.PerformAttack();
        _playerHealth.UpdatePlayerHealthBar();

        // damage test DELETE
        if (Input.GetKeyDown(KeyCode.U))
        {
            _playerHealth.TakeDamage(1);
        }

        // heal test DELETE
        if (Input.GetKeyDown(KeyCode.I))
        {
            _playerHealth.Heal(2);
        }

        // DELETE
        if (Input.GetKeyDown(KeyCode.T))
        {
            isMobileUIActive = !isMobileUIActive;
            _mobileUI.SetActive(isMobileUIActive);
        }

    }

    //private void ProcessMechanics()
    //{
    //    if (_playerMovement.IsGrounded() && !_playerControls.Player.Jump.triggered)
    //    {
    //        _playerMovement.jumpCounter = 0f;
    //        _playerMovement.IsJumpingMidAir = false;
    //        _playerMovement._currentJumpAmount = _playerMovement._baseJumpAmount;
    //    }

    //    if (_playerMovement.IsGrounded())
    //    {
    //        _playerMovement.HangTimeCounter = _playerMovement.HangTime;
    //    }
    //    else
    //    {
    //        _playerMovement.HangTimeCounter -= Time.deltaTime;
    //    }

    //    if (_playerControls.Player.Jump.triggered)
    //    {
    //        _playerMovement.JumpBufferCounter = _playerMovement.JumpBufferLength;

    //        if (_playerMovement.JumpBufferCounter > 0f && (_playerMovement.HangTimeCounter > 0f || _playerMovement.jumpCounter < _playerMovement._currentJumpAmount))
    //        {
    //            _playerMovement.JumpRequest = true;
    //            _playerMovement.jumpCounter++;
    //        }
    //    }
    //    else
    //    {
    //        if (_playerMovement.JumpBufferCounter > -2f)
    //        {
    //            _playerMovement.JumpBufferCounter -= Time.deltaTime;

    //        }
    //    }



    //    if (_playerControls.Player.Dash.triggered && _playerDash.CanDash)
    //    {
    //        _playerDash.DashRequest = true;
    //    }
    //}

    private void FixedUpdate()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        _playerHealth.UpdateHitTimer();
        _playerAttack.UpdateAttackTimer();
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.TakeDamage(damage);
        Debug.Log("Player taking damage is broken in the enemy scripts");
        DamagePopup.Create(transform.position + Vector3.right + Vector3.up, (int)damage);
    }

}
