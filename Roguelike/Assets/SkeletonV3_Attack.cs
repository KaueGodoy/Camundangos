using System;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonV3_Attack : MonoBehaviour
{
    public event EventHandler OnSkeletonAttacAnimation;

    private NewPlayerController _player;
    private Transform _target;

    [Header("Attack")]
    [SerializeField] private float _attackRange = 5f;

    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    private bool _canAttack = false;
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }

    private void Start()
    {
        _player = NewPlayerController.Instance;
        _target = _player.transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _target.position);

        if (distance < AttackRange)
        {
            CanAttack = true;
            //TriggerAttackAnimation();
        }
        else
        {
            CanAttack = false;
        }
    }

    private void TriggerAttackAnimation()
    {
        OnSkeletonAttacAnimation?.Invoke(this, EventArgs.Empty);
    }
}
