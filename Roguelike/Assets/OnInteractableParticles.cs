using UnityEngine;

public class OnInteractableParticles : MonoBehaviour
{
    [SerializeField] private InteractableObject_AddToInventory _interactableAddToInventory;
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        _interactableAddToInventory.OnInteractionParticles += _interactableAddToInventory_OnInteractionParticles;

        ParticleManager.Instance.HideParticle(_particle);
    }

    private void _interactableAddToInventory_OnInteractionParticles(object sender, System.EventArgs e)
    {
        ParticleManager.Instance.ExecuteParticle(_particle);
    }

    private void OnDestroy()
    {
        _interactableAddToInventory.OnInteractionParticles -= _interactableAddToInventory_OnInteractionParticles;
    }
}
