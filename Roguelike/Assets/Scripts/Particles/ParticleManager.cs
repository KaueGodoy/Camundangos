using System.Collections;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [Header("Dependency")]
    [SerializeField] private ChangeCharacterController _changeCharacterController;

    [Header("Particles")]
    [SerializeField] private GameObject _walkParticle;
    [SerializeField] private ParticleSystem _onCharacterChangedParticle;
    [SerializeField] private ParticleSystem _onJumpParticle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _changeCharacterController.OnCharacterChangedParticles += _changeCharacterController_OnCharacterChangedParticles;
        NewPlayerMovement.Instance.OnJumpParticlesTriggered += NewPlayerMovement_OnJumpParticlesTriggered;


        HideParticle(_onCharacterChangedParticle);
        HideParticle(_onJumpParticle);
    }

    private void NewPlayerMovement_OnJumpParticlesTriggered(object sender, System.EventArgs e)
    {
        ExecuteParticle(_onJumpParticle);
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

    private IEnumerator LerpSpriteRendererAlpha(float targetAlpha, float duration, SpriteRenderer spriteRenderer)
    {
        Color currentColor = spriteRenderer.color;
        float startAlpha = currentColor.a;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

    public void CallLerpSpriteRendererAlphaCoroutine(float targetAlpha, float duration, SpriteRenderer spriteRenderer)
    {
        StartCoroutine(LerpSpriteRendererAlpha(targetAlpha, duration, spriteRenderer));
    }


    private void OnDestroy()
    {
        _changeCharacterController.OnCharacterChangedParticles -= _changeCharacterController_OnCharacterChangedParticles;
    }
}
