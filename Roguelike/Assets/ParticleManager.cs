using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("Dependency")]
    [SerializeField] private ChangeCharacterController _changeCharacterController;

    [Header("Particles")]
    [SerializeField] private GameObject _walkParticle;

    [SerializeField] private ParticleSystem _onCharacterChangedParticle;

    private void Start()
    {
        _changeCharacterController.OnCharacterChangedParticles += _changeCharacterController_OnCharacterChangedParticles;
    }

    private void _changeCharacterController_OnCharacterChangedParticles(object sender, System.EventArgs e)
    {
        PlayParticleOneShot(_onCharacterChangedParticle);
        Debug.Log("Changed character... playing particle");
    }

    private void PlayParticleOneShot(ParticleSystem particle)
    {
        particle.Play();
    }

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
