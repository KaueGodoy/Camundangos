using Unity.VisualScripting;
using UnityEngine;

public class EnemyController_Skeleton : MonoBehaviour
{
    [SerializeField] private bool _isInAttackRange;
    [SerializeField] private float _rangeValue = 10f;
    [SerializeField] private float _distance;

    private Transform _playerTransform;
    private EnemyBehavior_FireProjectile _fireProjectileBehavior;

    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;

        _fireProjectileBehavior = GetComponent<EnemyBehavior_FireProjectile>();
    }

    public bool IsInAttackRange
    {
        get
        {
            if (_distance <= _rangeValue)
            {
                _isInAttackRange = true;
            }
            else
            {
                _isInAttackRange = false;
            }

            return _isInAttackRange;
        }
    }

    void Update()
    {
        _distance = transform.position.x - _playerTransform.transform.position.x;

        if (IsInAttackRange)
        {
            _fireProjectileBehavior.Attack();
        }
        else
        {
            _fireProjectileBehavior.CancelAttack();
        }
    }
}
