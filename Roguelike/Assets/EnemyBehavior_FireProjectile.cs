using UnityEngine;

public class EnemyBehavior_FireProjectile : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private Transform _projectilePrefab;
    private Transform _projectile;

    [Header("Projectile")]
    [SerializeField] private float _attackDuration = 0.9f;
    [SerializeField] private float _shotRate = 0.9f;

    public void Attack()
    {
        if (!IsInvoking("InstantiateProjectile"))
        {
            InvokeRepeating("InstantiateProjectile", _attackDuration, _shotRate);
        }
    }

    public void CancelAttack()
    {
        if (IsInvoking("InstantiateProjectile"))
        {
            CancelInvoke();
        }
    }

    private void InstantiateProjectile()
    {
        _projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

}
