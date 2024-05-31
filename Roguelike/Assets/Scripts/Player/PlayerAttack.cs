using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance { get; private set; }

    #region Attack

    [Header("Attack")]
    private float attackTimer = 0.0f;

    public float attackDelay = 0.4f;
    public float timeSinceAttack = 0.0f;
    public float attackStringReset = 0.8f;

    public static int CurrentAttack { get; set; }

    public bool AttackRequest { get; set; }
    public bool attackString = false;

    public static bool AttackAnimation { get; set; }
    public bool IsAttacking { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        AttackRequest = true;
        PerformAttack();
    }

    public void PerformAttack()
    {
        if (AttackRequest)
        {
            AttackRequest = false;
            AttackAnimation = true;
            attackString = true;

            if (!IsAttacking)
            {
                IsAttacking = true;

                UpdateAttackString();

                Invoke("AttackComplete", attackDelay);

                ResetAttackString();
            }
        }
    }

    private void Update()
    {
        UpdateAttackTimer();
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
        Debug.Log("Attack string: " + CurrentAttack);

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
        IsAttacking = false;
    }

    #endregion
}
