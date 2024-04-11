using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnCharacterUnlockedVisual : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _onDestroyParticle;
    [Header("Fader Lerp Settings")]
    [SerializeField] private float _targetAlpha = 0f;
    [SerializeField] private float _lerpDuration = 1.5f;

    public void HideParticlesOnStart()
    {
        ParticleManager.Instance.HideParticle(_onDestroyParticle);
    }

    public void PlayVisualParticles()
    {
        ParticleManager.Instance.ExecuteParticle(_onDestroyParticle);
        ParticleManager.Instance.CallLerpSpriteRendererAlphaCoroutine(_targetAlpha, _lerpDuration, _spriteRenderer);
    }
}
