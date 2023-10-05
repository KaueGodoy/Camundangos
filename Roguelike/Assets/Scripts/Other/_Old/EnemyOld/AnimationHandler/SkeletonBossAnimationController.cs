using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossAnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Skeleton skeleton;
    private EnemyChaseAttack enemyBehavior;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<Skeleton>();
        animator = GetComponent<Animator>();
        enemyBehavior = GetComponent<EnemyChaseAttack>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private Animator animator;
    private string currentAnimation;

    private const string SkeletonBossIdle = "skeleton_idle";
    private const string SkeletonBossWalk = "skeleton_walk";
    private const string SkeletonBossAttack = "skeleton_attack";
    private const string SkeletonBossHit = "skeleton_hit";
    private const string SkeletonBossDeath = "skeleton_death";

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
            ChangeAnimationState(SkeletonBossDeath);
        }
        // hit
        else if (skeleton.isHit)
        {
            ChangeAnimationState(SkeletonBossHit);
        }
        // attack
        else if (enemyBehavior.attackAnimation)
        {
            ChangeAnimationState(SkeletonBossAttack);
        }
        // move
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            ChangeAnimationState(SkeletonBossWalk);
            // Mathf.Abs(rb.velocity.x) > Mathf.Epsilon
            // rb.velocity.x > 0 || rb.velocity.x < 0
        }
        // idle
        else
        {
            ChangeAnimationState(SkeletonBossIdle);
        }

    }

    /*
    private void UpdateAnimationState()
    {
        // death
        if (skeleton.isDead())
        {
            AnimationHandler.PlayAnimation(SkeletonBossDeath);
        }
        // hit
        else if (skeleton.isHit)
        {
            AnimationHandler.PlayAnimation(SkeletonBossHit);
        }
        // attack
        else if (enemyBehavior.attackAnimation)
        {
            AnimationHandler.PlayAnimation(SkeletonBossAttack);
        }
        // move
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            AnimationHandler.PlayAnimation(SkeletonBossWalk);
        }
        // idle
        else
        {
            AnimationHandler.PlayAnimation(SkeletonBossIdle);
        }

    }*/

}
