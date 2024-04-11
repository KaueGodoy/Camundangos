using UnityEngine;

public class OnMatiasUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _matias;

    private void Start()
    {
        HideParticlesOnStart();
        _matias.OnMatiasUnlockedVisual += _Character_OnMatiasUnlockedVisual;
    }

    private void _Character_OnMatiasUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
