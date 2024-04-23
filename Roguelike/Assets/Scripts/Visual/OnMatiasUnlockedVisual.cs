using UnityEngine;

public class OnMatiasUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _matias;

    public override void Awake() => base.Awake();


    public override void Start()
    {
        base.Start();

        _matias.OnMatiasUnlockedVisual += _Character_OnMatiasUnlockedVisual;
    }

    private void _Character_OnMatiasUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
