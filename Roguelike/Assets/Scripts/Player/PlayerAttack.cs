using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    #region Attack

    [Header("Attack")]
    private float attackTimer = 0.0f;

    public float attackDelay = 0.4f;
    public float timeSinceAttack = 0.0f;
    public float attackStringReset = 0.8f;

    public int CurrentAttack = 0;

    public bool attackRequest = false;
    public bool attackString = false;

    public bool AttackAnimation = false;
    private bool isAttacking = false;

    public void PerformAttack()
    {
        if (attackRequest)
        {
            attackRequest = false;
            AttackAnimation = true;
            attackString = true;

            if (!isAttacking)
            {
                isAttacking = true;

                UpdateAttackString();

                Invoke("AttackComplete", attackDelay);

                ResetAttackString();
            }
        }
    }

    public void UpdateAttackTimer()
    {
        if (AttackAnimation)
            attackTimer += Time.deltaTime;

        if (attackTimer > attackDelay)
        {
            AttackAnimation = false;
            attackTimer = 0f;
        }

        if (attackString)
            timeSinceAttack += Time.deltaTime;

        if (timeSinceAttack > attackStringReset)
        {
            attackString = false;
            CurrentAttack = 0;
        }
    }

    public void UpdateAttackString()
    {
        CurrentAttack++;
        //Debug.Log("Attacking");

        // loops attack string
        if (CurrentAttack > 3)
        {
            CurrentAttack = 1;
        }
    }

    public void ResetAttackString()
    {
        timeSinceAttack = 0.0f;
    }

    private void AttackComplete()
    {
        isAttacking = false;
    }

    #endregion
}
