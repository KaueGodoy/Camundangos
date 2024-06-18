using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private BaseEnemy _baseEnemy;
    [SerializeField] private SkeletonV3_Attack _skeletonV3Attack;

    private Animator _animator;
    private string currentAnimation;

    private string EnemyIdle => $"{_baseEnemy.gameObject.name}_Idle";
    private string EnemyWalk => $"{_baseEnemy.gameObject.name}_Walk";
    private string EnemyAttack => $"{_baseEnemy.gameObject.name}_Attack";
    private string EnemyHit => $"{_baseEnemy.gameObject.name}_Hit";
    private string EnemyDeath => $"{_baseEnemy.gameObject.name}_Death";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //Debug.Log(EnemyIdle);
    }

    private void Update()
    {
        UpdateAnimationState(); 
    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        if (_baseEnemy.isDead())
        {
            ChangeAnimationState(EnemyDeath);
        }
        else if (_baseEnemy.IsHit)
        {
            ChangeAnimationState(EnemyHit);
        }
        else if (_skeletonV3Attack.CanAttack)
        {
            ChangeAnimationState(EnemyAttack);
        }
        else if (_baseEnemy.RigidBody.velocity.x > 0 || _baseEnemy.RigidBody.velocity.x < 0)
        {
            ChangeAnimationState(EnemyWalk);
        }
        else
        {
            ChangeAnimationState(EnemyIdle);
        }
    }
}
