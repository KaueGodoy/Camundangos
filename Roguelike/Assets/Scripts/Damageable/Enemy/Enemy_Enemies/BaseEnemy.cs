using UnityEngine;

public class BaseEnemy : DamageableWithHealthBar
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D RigidBody { get { return _rigidbody; } set { _rigidbody = value; } }

    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool isDead()
    {
        return CurrentHealth <= HealthThreshold;
    }

    public override void Die()
    {
        RigidBody.bodyType = RigidbodyType2D.Static;
        _boxCollider.enabled = false;

        Debug.Log("Base enemy dead");
        base.Die();
    }

}
