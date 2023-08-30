using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{

    #region Skill

    [Header("Skill")]
    public Transform Firepoint;
    public Transform SpawnPoint;
    public GameObject pfProjectile;

    private float skillAttackTimer = 0.0f;

    public float skillAttackDelay = 0.4f;
    public float skillTimeSinceAttack = 0.0f;

    public bool skillAttackRequest = false;

    public bool skillAttackAnimation = false;
    private bool skillIsAttacking = false;

    private PlayerCooldowns _playerCooldowns;

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
    }

    public void PerformSkill()
    {
        //if (skillCooldowns == null) return;

        if (_playerCooldowns.offCooldown)
        {
            if (skillAttackRequest)
            {
                skillAttackRequest = false;
                skillAttackAnimation = true;

                if (!skillIsAttacking)
                {
                    skillIsAttacking = true;

                    Invoke("InstantiateSkill", skillAttackDelay - 0.1f);
                    Invoke("SkillComplete", skillAttackDelay);
                }
            }
        }
        else
        {
            skillAttackRequest = false;
        }
    }

    private void InstantiateSkill()
    {
        Instantiate(pfProjectile, Firepoint.position, Firepoint.rotation);
        FindObjectOfType<AudioManager>().PlaySound("Attack");
    }

    private void SkillComplete()
    {
        skillIsAttacking = false;
    }

    public void UpdateSkillTimer()
    {
        if (skillAttackAnimation)
            skillAttackTimer += Time.deltaTime;

        if (skillAttackTimer > skillAttackDelay)
        {
            skillAttackAnimation = false;
            skillAttackTimer = 0f;
        }
    }

    #endregion
}
