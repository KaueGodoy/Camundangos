using UnityEngine;

public class Potion_IncreaseCurrentHealth : Consumable
{
    [SerializeField] private float _healAmount;
    public float HealAmount { get { return _healAmount; } set { _healAmount = value; } }

    public override void Consume(CharacterStats stats)
    {
        base.Consume(stats);
        NewPlayerController.Instance.HealPlayer(HealAmount);
        Debug.Log("Increasing current health");
    }
}
