using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShortRangeAnimationController : MonoBehaviour
{
    [SerializeField] private SkeletonV2 _skeleton;

    private EnemyPatrolChaseAttack _enemyBehavior;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();

        _rb = _skeleton.GetComponent<Rigidbody2D>();
        _enemyBehavior = _skeleton.GetComponent<EnemyPatrolChaseAttack>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private Animator _animator;
    private string currentAnimation;

    private const string SkeletonIdle = "skeletonv2_idle";
    //private const string SkeletonWalk = "skeletonv2_walk";
    private const string SkeletonAttack = "skeletonv2_attack";
    private const string SkeletonHit = "skeletonv2_hit";
    private const string SkeletonDeath = "skeletonv2_death";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        if (_skeleton.isDead())
        {
            ChangeAnimationState(SkeletonDeath);
        }
        else if (_skeleton.IsHit)
        {
            ChangeAnimationState(SkeletonHit);
        }
        else if (_enemyBehavior.IsAttackAnimationPlaying)
        {
            ChangeAnimationState(SkeletonAttack);
        }
        else if (_rb.velocity.x > 0 || _rb.velocity.x < 0)
        {
            ChangeAnimationState(SkeletonIdle);
        }
        else
        {
            ChangeAnimationState(SkeletonIdle);
        }
    }
}
