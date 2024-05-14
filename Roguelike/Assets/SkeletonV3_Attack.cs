using System;
using UnityEngine;

public class SkeletonV3_Attack : MonoBehaviour
{
    public event EventHandler OnSkeletonAttacAnimation;

    private NewPlayerController _player;
    private Transform _target;

    [Header("Attack")]
    [SerializeField] private float _attackRange = 5f;

    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

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
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        OnSkeletonAttacAnimation?.Invoke(this, EventArgs.Empty);
    }
}
