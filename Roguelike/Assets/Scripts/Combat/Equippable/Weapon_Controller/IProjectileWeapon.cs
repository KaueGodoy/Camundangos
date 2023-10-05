using UnityEngine;

public interface IProjectileWeapon
{
    public Transform ProjectileSpawn { get; set; }
    void CastProjectile();
}
