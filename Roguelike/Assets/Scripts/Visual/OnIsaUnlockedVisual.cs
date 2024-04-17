using UnityEngine;

public class OnIsaUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _isa;

    private void Start()
    {
        HideParticlesOnStart();
        _isa.OnIsaUnlockedVisual += Character_OnIsaUnlockedVisual;
    }

    private void Character_OnIsaUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
