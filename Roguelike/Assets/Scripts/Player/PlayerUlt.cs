using UnityEngine;

public class PlayerUlt : MonoBehaviour
{
    private PlayerCooldowns _playerCooldowns;
    private AudioManager _audioManager;

    [Header("Ult")]
    [SerializeField] private Transform UltSpawnPoint;
    [SerializeField] private GameObject Projectile;

    [SerializeField] private float _ultAttackDelay = 0.4f;
    [SerializeField] private float _ultAttackTimer = 0.0f;

    private bool _isUltPerformed = false;

    public bool UltAttackRequest { get; set; }
    public static bool UltAttackAnimation { get; set; }

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerUlt += GameInput_OnPlayerUlt;
        UltAttackRequest = false;
    }

    private void GameInput_OnPlayerUlt(object sender, System.EventArgs e)
    {
        UltAttackRequest = true;
        PerformUlt();
    }

    private void Update()
    {
        UpdateUltTimer();
    }

    public void PerformUlt()
    {
        if (_playerCooldowns.ultOffCooldown)
        {
            if (UltAttackRequest)
            {
                UltAttackRequest = false;
                UltAttackAnimation = true;
                NewPlayerMovement.Instance.IsControlLocked = true;

                if (!_isUltPerformed)
                {
                    _isUltPerformed = true;

                    InstantiateUlt();
                    Invoke("UltComplete", _ultAttackDelay);
                }
            }
        }
        else
        {
            UltAttackRequest = false;
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
        NewPlayerMovement.Instance.IsControlLocked = false;
    }

    public void UpdateUltTimer()
    {
        if (UltAttackAnimation)
            _ultAttackTimer += Time.deltaTime;

        if (_ultAttackTimer > _ultAttackDelay)
        {
            UltAttackAnimation = false;
            _ultAttackTimer = 0f;
        }
    }
}
