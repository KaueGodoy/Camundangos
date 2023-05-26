using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossAnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Skeleton skeleton;
    private EnemyChaseAttack enemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<Skeleton>();
        animator = GetComponent<Animator>();
        enemyBehavior = GetComponent<EnemyChaseAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // hit
        if (skeleton.isHit)
        {
            skeleton.hitTimer += Time.deltaTime;
        }

        if (skeleton.hitTimer > skeleton.hitCooldown)
        {
            skeleton.isHit = false;
            skeleton.hitTimer = 0f;
        }

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
        }
        // idle
        else
        {
            ChangeAnimationState(SkeletonBossIdle);
        }

    }

}
