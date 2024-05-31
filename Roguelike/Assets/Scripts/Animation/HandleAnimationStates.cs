using System;
using UnityEngine;

public class HandleAnimationStates : AnimationController
{
    [Header("Dependencies")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TextAsset _file;
    [SerializeField] private string[] _animation;
    [SerializeField] private Animator _animator;

    //[SerializeField] private string _death;
    //[SerializeField] private string _hit;
    //[SerializeField] private string _attackString01;
    //[SerializeField] private string _attackString02;
    //[SerializeField] private string _attackString03;
    //[SerializeField] private string _skill;
    //[SerializeField] private string _ult;
    //[SerializeField] private string _jump;
    //[SerializeField] private string _fall;
    //[SerializeField] private string _walk;
    //[SerializeField] private string _idle;

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
        UpdateTimer();
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
                ChangeAnimationState(_animation[2]);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (PlayerAttack.CurrentAttack == 3)
            {
                ChangeAnimationState(_animation[2]);
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
        else if (_playerMovement.Rigidbody.velocity.y > .1f && !_playerMovement.IsGrounded())
        {
            ChangeAnimationState(_animation[7]);
        }
        // fall
        else if (_playerMovement.Rigidbody.velocity.y < .1f && !_playerMovement.IsGrounded())
        {
            ChangeAnimationState(_animation[8]);
        }
        // move
        else if (_playerMovement.MoveH.x > 0 || _playerMovement.MoveH.x < 0)
        {
            ChangeAnimationState(_animation[9]);
        }
        // idle
        else
        {
            ChangeAnimationState(_animation[10]);
        }
    }

    private void UpdateTimer()
    {

    }
}

public enum AnimationState
{
    Idle, Walking
}
