using UnityEngine;

public class OnIsaUnlockedVisual : OnCharacterUnlockedVisual
{
    [SerializeField] private UnlockableCharacter _isa;

    public override void Awake() => base.Awake();


    public override void Start()
    {
        base.Start();

        _isa.OnIsaUnlockedVisual += Character_OnIsaUnlockedVisual;
    }

    private void Character_OnIsaUnlockedVisual(object sender, System.EventArgs e)
    {
        PlayVisualParticles();
    }
}
