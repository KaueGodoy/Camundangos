using UnityEngine;

public class OnLeoUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _leo;

    public override void Awake() => base.Awake();

    public override void Start()
    {
        base.Start();

        _leo.OnLeoUnlockedVisual += _Character_OnLeoUnlockedVisual;
    }

    private void _Character_OnLeoUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
