using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnCharacterUnlockedVisual : MonoBehaviour
{
    [SerializeField] private UnlockableCharacter _isa;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private ParticleSystem _onDestroyParticle;

    [SerializeField] private float _targetAlpha = 0f;
    [SerializeField] private float _lerpDuration = 1.5f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        OnCharacterUnlocked.Instance.OnCharacterUnlockedVisual += OnCharacterUnlocked_OnCharacterUnlockedVisual;

        ParticleManager.Instance.HideParticle(_onDestroyParticle);

        _isa.OnIsaUnlockedVisual += Character_OnIsaUnlockedVisual;
    }

    private void Character_OnIsaUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }

    private void OnCharacterUnlocked_OnCharacterUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }

    private void PlayVisualParticles()
    {
        ParticleManager.Instance.ExecuteParticle(_onDestroyParticle);
        ParticleManager.Instance.CallLerpSpriteRendererAlphaCoroutine(_targetAlpha, _lerpDuration, _spriteRenderer);
    }

    private void OnDestroy()
    {
        OnCharacterUnlocked.Instance.OnCharacterUnlockedVisual -= OnCharacterUnlocked_OnCharacterUnlockedVisual;
    }
}
