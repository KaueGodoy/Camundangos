using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Slime slime;
    private EnemyChaseAttack enemyBehavior;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slime = GetComponent<Slime>();
        animator = GetComponent<Animator>();
        enemyBehavior = GetComponent<EnemyChaseAttack>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    private Animator animator;
    private string currentAnimation;

    private const string SlimeIdle = "slime_idle";
    private const string SlimeWalk = "slime_walk";
    private const string SlimeAttack = "slime_attack";
    private const string SlimeHit = "slime_hit";
    private const string SlimeDeath = "slime_death";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        // death
        if (slime.isDead())
        {
            ChangeAnimationState(SlimeDeath);
        }
        // hit
        else if (slime.IsHit)
        {
            ChangeAnimationState(SlimeHit);
        }
        // attack
        else if (enemyBehavior.attackAnimation)
        {
            ChangeAnimationState(SlimeAttack);
        }
        // move
        else if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            ChangeAnimationState(SlimeWalk);
        }
        // idle
        else
        {
            ChangeAnimationState(SlimeIdle);
        }

    }

}
