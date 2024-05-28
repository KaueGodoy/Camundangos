using UnityEngine;

public class EnemyPatrolChaseAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer _boneSpriteRenderer;

    private NewPlayerController _player;
    private Transform _target;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _player = NewPlayerController.Instance;
        _target = _player.transform;
    }

    private void Update()
    {
        UpdateCooldowns();

        float distance = Vector2.Distance(transform.position, _target.position);
        direction = (_target.position - transform.position).normalized;

        if (distance < AttackRange)
        {
            isChasing = false;
            isPatrolling = false;
            Attack();

        }
        else if (distance < chaseRange)
        {
            isChasing = true;
            isPatrolling = false;
            Chase();
        }
        else
        {
            isChasing = false;
            isPatrolling = true;
            Patrol();
        }

        FlipSprite();
    }

    private void UpdateCooldowns()
    {
        // attack
        if (IsAttackAnimationPlaying)
        {
            _attackTimer += Time.deltaTime;
        }
        if (_attackTimer > _attackDelay)
        {
            IsAttackAnimationPlaying = false;
            _attackTimer = 0f;
        }
    }

    #region Movement

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public bool isChasing = false;
    public bool isFacingRight;
    public bool isPatrolling;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        direction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        Move();

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

    }

    private void Chase()
    {
        Move();
        CancelInvoke();
    }

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = direction * moveSpeed;
        }
    }

    private void FlipSprite()
    {
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            //isFacingRight = !isFacingRight;
            //_boneSpriteRenderer.flipX = isFacingRight;

            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Idle()
    {
        direction = Vector2.zero;
        CancelInvoke("PerformAttack");
    }

    #endregion


    #region Attack

    [Header("Attack")]

    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float attackDuration = 0.9f;
    [SerializeField] private float attackRate = 0.9f;

    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }

    public bool IsAttackAnimationPlaying { get; set; }

    private float _attackTimer = 0.0f;
    private float _attackDelay = 0.85f;

    private void Attack()
    {
        IsAttackAnimationPlaying = true;

        if (!IsInvoking("AttackPlayer"))
        {
            InvokeRepeating("AttackPlayer", attackDuration, attackRate);
        }
    }

    public void AttackPlayer()
    {
        NewPlayerController.Instance.TakeDamage(Damage);
    }

    #endregion
}
