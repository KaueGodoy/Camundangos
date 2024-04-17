using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShortRangeAnimationController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Skeleton skeleton;
    [SerializeField] private EnemyPatrolChaseAttack enemyBehavior;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private Animator animator;
    private string currentAnimation;

    private const string SkeletonIdle = "skeletonv2_idle";
    //private const string SkeletonWalk = "skeletonv2_walk";
    private const string SkeletonAttack = "skeletonv2_attack";
    private const string SkeletonHit = "skeletonv2_hit";
    private const string SkeletonDeath = "skeletonv2_death";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        // death
        if (skeleton.isDead())
        {
            ChangeAnimationState(SkeletonDeath);
        }
        // hit
        else if (skeleton.isHit)
        {
            ChangeAnimationState(SkeletonHit);
        }
        // attack
        else if (enemyBehavior.attackAnimation)
        {
            ChangeAnimationState(SkeletonAttack);
        }
        // move
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            ChangeAnimationState(SkeletonIdle);
        }
        // idle
        else
        {
            ChangeAnimationState(SkeletonIdle);
        }

    }
}
