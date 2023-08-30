using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUlt : MonoBehaviour
{
    #region Ult

    [Header("Ult")]
    public Transform UltSpawnPoint;
    public GameObject pfUltProjectile;

    public float ultAttackDelay = 0.4f;
    private float ultAttackTimer = 0.0f;
    public float ultTimeSinceAttack = 0.0f;

    public bool ultAttackRequest = false;
    public bool ultAttackAnimation = false;
    private bool isUlting = false;

    private PlayerCooldowns _playerCooldowns;

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
    }

    public void PerformUlt()
    {
        if (_playerCooldowns.ultOffCooldown)
        {
            if (ultAttackRequest)
            {
                ultAttackRequest = false;
                ultAttackAnimation = true;

                if (!isUlting)
                {
                    isUlting = true;

                    Invoke("InstantiateUlt", ultAttackDelay - 0.1f);
                    Invoke("UltComplete", ultAttackDelay);
                }
            }
        }
        else
        {
            ultAttackRequest = false;
        }
    }

    private void InstantiateUlt()
    {
        Instantiate(pfUltProjectile, UltSpawnPoint.position, UltSpawnPoint.rotation);
        FindObjectOfType<AudioManager>().PlaySound("Attack");
    }

    private void UltComplete()
    {
        isUlting = false;
    }

    public void UpdateUltimer()
    {
        if (ultAttackAnimation)
            ultAttackTimer += Time.deltaTime;

        if (ultAttackTimer > ultAttackDelay)
        {
            ultAttackAnimation = false;
            ultAttackTimer = 0f;
        }
    }

    #endregion
}
