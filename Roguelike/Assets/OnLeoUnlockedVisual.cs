using UnityEngine;

public class OnLeoUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _leo;

    private void Start()
    {
        HideParticlesOnStart();
        _leo.OnLeoUnlockedVisual += _Character_OnLeoUnlockedVisual;
    }

    private void _Character_OnLeoUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
