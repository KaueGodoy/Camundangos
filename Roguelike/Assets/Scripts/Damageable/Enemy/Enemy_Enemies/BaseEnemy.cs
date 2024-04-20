using UnityEngine;

public class BaseEnemy : DamageableWithHealthBar
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();   
    }

    public bool isDead()
    {
        return CurrentHealth <= HealthThreshold;
    }

    public override void Die()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _boxCollider.enabled = false;

        Debug.Log("Base enemy dead");
        base.Die();
    }

}
