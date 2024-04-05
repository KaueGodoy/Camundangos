using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [Header("Dependency")]
    [SerializeField] private ChangeCharacterController _changeCharacterController;

    [Header("Particles")]
    [SerializeField] private GameObject _walkParticle;
    [SerializeField] private ParticleSystem _onCharacterChangedParticle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _changeCharacterController.OnCharacterChangedParticles += _changeCharacterController_OnCharacterChangedParticles;

        HideParticle(_onCharacterChangedParticle);
    }

    private void _changeCharacterController_OnCharacterChangedParticles(object sender, System.EventArgs e)
    {
        ExecuteParticle(_onCharacterChangedParticle);
    }

    public void ExecuteParticle(ParticleSystem particle)
    {
        ShowParticle(particle);
        PlayParticleOneShot(particle);
    }

    public void PlayParticleOneShot(ParticleSystem particle)
    {
        particle.Play();
    }

    public void HideParticle(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
    }

    public void ShowParticle(ParticleSystem particle)
    {
        particle.gameObject.SetActive(true);
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

    private void OnDestroy()
    {
        _changeCharacterController.OnCharacterChangedParticles -= _changeCharacterController_OnCharacterChangedParticles;
    }
}
