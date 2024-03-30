using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject _walkParticle;

    private void FixedUpdate()
    {
        ToggleWalkParticle();
    }

    private void ToggleWalkParticle()
    {
        if (!NewPlayerMovement.Instance.IsGrounded())
        {
            _walkParticle.gameObject.SetActive(false);
        }
        else
        {
            _walkParticle.gameObject.SetActive(true);
        }
    }
}
