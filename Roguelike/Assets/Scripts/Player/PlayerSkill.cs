using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerCooldowns _playerCooldowns;

    private void Awake()
    {
        _playerCooldowns = GetComponent<PlayerCooldowns>();
    }

    #region Skill

    [Header("Skill")]
    public Transform Firepoint;
    public Transform SpawnPoint;
    public GameObject pfProjectile;

    private float _skillAttackTimer = 0.0f;
    private float _skillAttackDelay = 0.4f;
    private bool _isSkillPerforming = false;

    public bool skillAttackRequest = false;
    public bool skillAttackAnimation = false;

    public void PerformSkill()
    {
        if (_playerCooldowns.offCooldown)
        {
            if (skillAttackRequest)
            {
                skillAttackRequest = false;
                skillAttackAnimation = true;

                if (!_isSkillPerforming)
                {
                    _isSkillPerforming = true;

                    Invoke("InstantiateSkill", _skillAttackDelay - 0.1f);
                    Invoke("SkillComplete", _skillAttackDelay);
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
        _isSkillPerforming = false;
    }

    public void UpdateSkillTimer()
    {
        if (skillAttackAnimation)
            _skillAttackTimer += Time.deltaTime;

        if (_skillAttackTimer > _skillAttackDelay)
        {
            skillAttackAnimation = false;
            _skillAttackTimer = 0f;
        }
    }

    #endregion
}
