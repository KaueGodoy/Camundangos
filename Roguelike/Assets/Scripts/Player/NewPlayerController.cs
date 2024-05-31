using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public static NewPlayerController Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private ParticleSystem _playerHitParticle;

    [Header("Mobile")]
    [SerializeField] private GameObject _mobileUI;
    private bool isMobileUIActive;
    //[SerializeField] private ParticleSystem _particle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerHealth.UpdatePlayerHealthBar();
    }

    private void Update()
    {
        if (!PlayerHealth.IsAlive) return;

        //_playerAttack.PerformAttack();
        _playerHealth.UpdatePlayerHealthBar();

        // damage test DELETE
        if (Input.GetKeyDown(KeyCode.U))
        {
            _playerHealth.TakeDamage(1);
        }

        // heal test DELETE
        if (Input.GetKeyDown(KeyCode.I))
        {
            _playerHealth.Heal(2);
            //ParticleManager.Instance.InstantiateParticle(_particle, transform.position);
        }

        // DELETE
        if (Input.GetKeyDown(KeyCode.T))
        {
            isMobileUIActive = !isMobileUIActive;
            _mobileUI.SetActive(isMobileUIActive);
        }

    }

    private void FixedUpdate()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        _playerHealth.UpdateHitTimer();
        //_playerAttack.UpdateAttackTimer();
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.TakeDamage(damage);
        Debug.Log("Player taking damage is broken in the enemy scripts");
        AudioManager.Instance.PlaySound("OnPlayerHit_SFX");
        DamagePopup.Create(transform.position + Vector3.right + Vector3.up, (int)damage);
        ParticleManager.Instance.InstantiateParticle(_playerHitParticle, transform.position);
    }

    public void HealPlayer(float healAmount)
    {
        _playerHealth.Heal(healAmount);
    }

}
