using UnityEngine;

public class EmitParticleOnTrigger : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        chest.OnChestOpenedParticles += Chest_OnChestOpenedParticles;

        ParticleManager.Instance.HideParticle(_particle);
    }

    private void Chest_OnChestOpenedParticles(object sender, System.EventArgs e)
    {
        ParticleManager.Instance.ExecuteParticle(_particle);
    }

    private void OnDestroy()
    {
        chest.OnChestOpenedParticles -= Chest_OnChestOpenedParticles;
    }
}
