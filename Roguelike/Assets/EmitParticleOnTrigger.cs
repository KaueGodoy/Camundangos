using UnityEngine;

public class EmitParticleOnTrigger : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        chest.OnChestOpenedParticles += Chest_OnChestOpenedParticles;
    }

    private void Chest_OnChestOpenedParticles(object sender, System.EventArgs e)
    {
        _particle.Play();
    }

    private void OnDestroy()
    {
        chest.OnChestOpenedParticles -= Chest_OnChestOpenedParticles;
    }
}
