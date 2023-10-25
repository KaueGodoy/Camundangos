using UnityEngine;

public class HandlePlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnimation;

    //private PlayerHealth _playerHealth;
    //private PlayerAttack _playerAttack;
    //private PlayerSkill _playerSkill;
    //private PlayerUlt _playerUlt;
    private PlayerMovement _playerMovement;

    private const string DerildoDeath = "derildo_death";
    private const string DerildoHit = "derildo_hit";
    private const string DerildoAttackString01 = "derildo_attack1";
    private const string DerildoAttackString02 = "derildo_attack2";
    private const string DerildoAttackString03 = "derildo_attack3";
    private const string DerildoSkill = "derildo_skill";
    private const string DerildoUlt = "derildo_ult";
    private const string DerildoJump = "derildo_jump";
    private const string DerildoFall = "derildo_fall";
    private const string DerildoWalk = "derildo_walk";
    private const string DerildoIdle = "derildo_idle";

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        //_playerHealth = GetComponent<PlayerHealth>();
        //_playerAttack = GetComponent<PlayerAttack>();
        //_playerSkill = GetComponent<PlayerSkill>();
        //_playerUlt = GetComponent<PlayerUlt>();
        _playerMovement = GetComponent<PlayerMovement>();

    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }

    public void UpdateAnimationState()
    {
        // death
        if (!PlayerHealth.IsAlive)
        {
            ChangeAnimationState(DerildoDeath);
        }
        // hit
        else if (PlayerHealth.IsHit)
        {
            ChangeAnimationState(DerildoHit);
        }
        // attack
        else if (PlayerAttack.AttackAnimation)
        {
            if (PlayerAttack.CurrentAttack == 1)
            {
                ChangeAnimationState(DerildoAttackString01);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (PlayerAttack.CurrentAttack == 2)
            {
                ChangeAnimationState(DerildoAttackString02);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
            else if (PlayerAttack.CurrentAttack == 3)
            {
                ChangeAnimationState(DerildoAttackString03);
                //Debug.Log("Attack string number: " + _playerAttack.CurrentAttack);

            }
        }
        // skill
        else if (PlayerSkill.skillAttackAnimation)
        {
            ChangeAnimationState(DerildoSkill);

        }
        // ult
        else if (PlayerUlt.ultAttackAnimation)
        {
            ChangeAnimationState(DerildoUlt);
        }
        // jump
        else if (_playerMovement.Rigidbody.velocity.y > .1f && !_playerMovement.IsGrounded())
        {
            ChangeAnimationState(DerildoJump);
        }
        // fall
        else if (_playerMovement.Rigidbody.velocity.y < .1f && !_playerMovement.IsGrounded())
        {
            ChangeAnimationState(DerildoFall);
        }
        // move
        else if (_playerMovement.MoveH.x > 0 || _playerMovement.MoveH.x < 0)
        {
            ChangeAnimationState(DerildoWalk);
        }
        // idle
        else
        {
            ChangeAnimationState(DerildoIdle);
        }
    }
}
