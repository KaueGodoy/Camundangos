using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerCooldowns _playerCooldowns;
    private AudioManager _audioManager;

    [Header("Skill")]
    public Transform SkillSpawnPoint;
    public GameObject Projectile;

    // DELETE 
    public Transform SpawnPoint;

    private float _skillAttackTimer = 0.0f;
    private float _skillAttackDelay = 0.4f;
    private bool _isSkillPerformed = false;

    public bool SkillAttackRequest { get; set; }
    public static bool SkillAttackAnimation { get; set; }

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerSkill += GameInput_OnPlayerSkill;
        SkillAttackRequest = false;
    }

    private void GameInput_OnPlayerSkill(object sender, System.EventArgs e)
    {
        if (NewPlayerMovement.Instance.IsControlLocked) return;

        SkillAttackRequest = true;
        PerformSkill();
    }

    private void Update()
    {
        UpdateSkillTimer();
    }

    public void PerformSkill()
    {
        if (_playerCooldowns.offCooldown)
        {
            if (SkillAttackRequest)
            {
                SkillAttackRequest = false;
                SkillAttackAnimation = true;

                if (!_isSkillPerformed)
                {
                    _isSkillPerformed = true;

                    Invoke("InstantiateSkill", _skillAttackDelay - 0.1f);
                    Invoke("SkillComplete", _skillAttackDelay);

                    Debug.Log("Skill performed");
                }
            }
        }
        else
        {
            SkillAttackRequest = false;
        }
    }

    private void InstantiateSkill()
    {
        Instantiate(Projectile, SkillSpawnPoint.position, SkillSpawnPoint.rotation);
        _audioManager.PlaySound("Attack");
    }

    private void SkillComplete()
    {
        _isSkillPerformed = false;
    }

    public void UpdateSkillTimer()
    {
        if (SkillAttackAnimation)
            _skillAttackTimer += Time.deltaTime;

        if (_skillAttackTimer > _skillAttackDelay)
        {
            SkillAttackAnimation = false;
            _skillAttackTimer = 0f;
        }
    }
}
