using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonV3ProjectileAnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SkeletonV3 skeleton;
    private EnemyShootRetreat enemyBehavior;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<SkeletonV3>();
        enemyBehavior = GetComponent<EnemyShootRetreat>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private string currentAnimation;

    private const string SkeletonIdle = "Skeleton_V3_Idle";
    private const string SkeletonWalk = "Skeleton_V3_Walk";
    private const string SkeletonAttack = "Skeleton_V3_Attack";
    private const string SkeletonProjectileAttack = "Skeleton_V3_ProjectileAttack";
    private const string SkeletonHit = "Skeleton_V3_Hit";
    private const string SkeletonDeath = "Skeleton_V3_Death";

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
        else if (skeleton.IsHit)
        {
            ChangeAnimationState(SkeletonHit);
        }
        // attack
        else if (enemyBehavior.attackAnimation)
        {
            ChangeAnimationState(SkeletonProjectileAttack);
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
