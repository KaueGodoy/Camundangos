using UnityEngine;

public class Potion_IncreaseMaxHP : Consumable
{
    public override void Consume(CharacterStats stats)
    {
        base.Consume(stats);
        Debug.Log("Increasing Max HP");
    }
}
