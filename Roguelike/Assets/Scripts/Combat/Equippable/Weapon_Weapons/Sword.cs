using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private bool _canAttack = false;
    [SerializeField] private float _attackAnimationRate = 0.4f;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _attackAnimationRate)
        {
            _canAttack = true;
        }
    }

    public override void PerformAttack(float damage)
    {
        if (!_canAttack) return;

        base.PerformAttack(damage);
        Animator.SetTrigger("Base_Attack");
        //Animator.Play("SwordSwing");
        _canAttack = false;
        _timer = 0;
    }
}
