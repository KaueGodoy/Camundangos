using UnityEngine;

public class Potion_LogToConsole : Consumable
{
    public override void Consume()
    {
        base.Consume();
        Debug.Log("Logging something to the console");
    }
}
