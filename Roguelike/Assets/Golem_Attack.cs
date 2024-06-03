using UnityEngine;

public class Golem_Attack : MonoBehaviour
{
    [SerializeField] private ParticleSystem _attackParticle;

    private void InstantiateParticlesOnAOEAttack()
    {
        ParticleManager.Instance.InstantiateParticle(_attackParticle, transform.position);
    }

    [Header("Damage")]
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _minRandomMultiplier = 1f;
    [SerializeField] private float _maxRandomMultiplier = 2f;

    public float Damage
    {
        get
        {
            float randomMultiplier = Random.Range(_minRandomMultiplier, _maxRandomMultiplier);

            return _damage * randomMultiplier;
        }

        set { _damage = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {
            NewPlayerController.Instance.TakeDamage(Damage);
            InstantiateParticlesOnAOEAttack();
        }
    }

}
