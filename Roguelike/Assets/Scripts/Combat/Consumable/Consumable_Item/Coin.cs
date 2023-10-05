using UnityEngine;

public class Coin : Consumable
{
    public override void Consume()
    {
        base.Consume();
        Debug.Log("Money~~");
    }
}
