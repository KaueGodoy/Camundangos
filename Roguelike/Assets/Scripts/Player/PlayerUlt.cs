using UnityEngine;

public class PlayerUlt : MonoBehaviour
{
    private PlayerCooldowns _playerCooldowns;
    private AudioManager _audioManager;

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    [Header("Ult")]
    public Transform UltSpawnPoint;
    public GameObject Projectile;

    private float _ultAttackDelay = 0.4f;
    private float _ultAttackTimer = 0.0f;

    private bool _isUltPerformed = false;

    public bool ultAttackRequest = false;
    public bool ultAttackAnimation = false;   

    public void PerformUlt()
    {
        if (_playerCooldowns.ultOffCooldown)
        {
            if (ultAttackRequest)
            {
                ultAttackRequest = false;
                ultAttackAnimation = true;

                if (!_isUltPerformed)
                {
                    _isUltPerformed = true;

                    Invoke("InstantiateUlt", _ultAttackDelay - 0.1f);
                    Invoke("UltComplete", _ultAttackDelay);
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
        Instantiate(Projectile, UltSpawnPoint.position, UltSpawnPoint.rotation);
        _audioManager.PlaySound("Attack");
    }

    private void UltComplete()
    {
        _isUltPerformed = false;
    }

    public void UpdateUltimer()
    {
        if (ultAttackAnimation)
            _ultAttackTimer += Time.deltaTime;

        if (_ultAttackTimer > _ultAttackDelay)
        {
            ultAttackAnimation = false;
            _ultAttackTimer = 0f;
        }
    }
}
