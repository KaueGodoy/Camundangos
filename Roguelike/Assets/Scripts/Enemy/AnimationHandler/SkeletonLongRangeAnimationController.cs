using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLongRangeAnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Skeleton skeleton;
    private EnemyShootRetreat enemyBehavior;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<Skeleton>();
        animator = GetComponent<Animator>();
        enemyBehavior = GetComponent<EnemyShootRetreat>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private Animator animator;
    private string currentAnimation;

    private const string skeletonIdle = "skeletonv2_idle";
    private const string SkeletonAttack = "skeletonv2_attack";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);    
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        // death
        //if (skeleton.isDead())
        //{
        //    ChangeAnimationState(SkeletonBossDeath);
        //}
        //// hit
        //else if (skeleton.isHit)
        //{
        //    ChangeAnimationState(SkeletonBossHit);
        //}
        //// attack
         if (enemyBehavior.attackAnimation)
        {
            ChangeAnimationState(SkeletonAttack);
        }
        //// move
        //else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        //{
        //    ChangeAnimationState(SkeletonBossWalk);
        //    // Mathf.Abs(rb.velocity.x) > Mathf.Epsilon
        //    // rb.velocity.x > 0 || rb.velocity.x < 0
        //}
        // idle
        else
        {
            ChangeAnimationState(skeletonIdle);
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
