using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnCharacterUnlockedVisual : MonoBehaviour
{
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
    }

    private void OnCharacterUnlocked_OnCharacterUnlockedVisual(object sender, System.EventArgs e)
    {

        ParticleManager.Instance.ExecuteParticle(_onDestroyParticle);
        ParticleManager.Instance.CallLerpSpriteRendererAlphaCoroutine(_targetAlpha, _lerpDuration, _spriteRenderer);
    }

    private void OnDestroy()
    {
        OnCharacterUnlocked.Instance.OnCharacterUnlockedVisual -= OnCharacterUnlocked_OnCharacterUnlockedVisual;
    }
}
