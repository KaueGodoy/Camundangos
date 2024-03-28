using System;
using Unity.VisualScripting;
using UnityEngine;

public class NewAnimationState : AnimationController
{
    [Header("Dependencies")]
    [SerializeField] private Animator _animator;
    [SerializeField] private TextAsset _file;
    [SerializeField] private string[] _animation;
    [SerializeField] private Vector2 _playerMoveDirection;

    private void Start()
    {
        Animator = _animator;
    }

    private void OnValidate()
    {
        LoadAnimationFile();
    }

    private void LoadAnimationFile()
    {
        _animation = _file ? _file.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        _playerMoveDirection.x = NewPlayerMovement.Instance.InputVector.x;
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (_file == null) return;

        // death
        if (!PlayerHealth.IsAlive)
        {
            ChangeAnimationState(_animation[0]);
        }
        // hit
        else if (PlayerHealth.IsHit)
        {
            ChangeAnimationState(_animation[1]);
        }
        // attack
        else if (PlayerAttack.AttackAnimation)
        {
            if (PlayerAttack.CurrentAttack == 1)
            {
                ChangeAnimationState(_animation[2]);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (PlayerAttack.CurrentAttack == 2)
            {
                ChangeAnimationState(_animation[3]);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (PlayerAttack.CurrentAttack == 3)
            {
                ChangeAnimationState(_animation[4]);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
        }
        // skill
        else if (PlayerSkill.SkillAttackAnimation)
        {
            ChangeAnimationState(_animation[5]);

        }
        // ult
        else if (PlayerUlt.UltAttackAnimation)
        {
            ChangeAnimationState(_animation[6]);
        }
        // jump
        else if (NewPlayerMovement.Instance.Rb.velocity.y > .1f && !NewPlayerMovement.Instance.IsGrounded())
        {
            ChangeAnimationState(_animation[7]);
        }
        // fall
        else if (NewPlayerMovement.Instance.Rb.velocity.y < .1f && !NewPlayerMovement.Instance.IsGrounded())
        {
            ChangeAnimationState(_animation[8]);
        }
        // move
        else if (_playerMoveDirection.x > 0f || _playerMoveDirection.x < 0f)
        {
            ChangeAnimationState(_animation[9]);
        }
        // idle
        else
        {
            ChangeAnimationState(_animation[10]);
        }
    }
}