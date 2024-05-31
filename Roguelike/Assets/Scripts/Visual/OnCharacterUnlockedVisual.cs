using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnCharacterUnlockedVisual : MonoBehaviour
{
    [Header("Dependencies")]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _onDestroyParticle;
    [Header("Fader Lerp Settings")]
    [SerializeField] private float _targetAlpha = 0f;
    [SerializeField] private float _lerpDuration = 1.5f;

    public virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    public virtual void Start()
    {
        HideParticlesOnStart();
    }

    public void HideParticlesOnStart()
    {
        ParticleManager.Instance.HideParticle(_onDestroyParticle);
    }

    public void PlayVisualParticles()
    {
        ParticleManager.Instance.ExecuteParticle(_onDestroyParticle);
        //ParticleManager.Instance.CallLerpSpriteRendererAlphaCoroutine(_targetAlpha, _lerpDuration, _spriteRenderer);
    }
}
