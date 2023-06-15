using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChildAnimationController : MonoBehaviour
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
        else
        {
            ChangeAnimationState(SkeletonIdle);
        }

    }
}
