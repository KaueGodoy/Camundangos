using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChildAnimationController : MonoBehaviour
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
        // idle
        else
        {
            ChangeAnimationState(SlimeIdle);
        }

    }

}
