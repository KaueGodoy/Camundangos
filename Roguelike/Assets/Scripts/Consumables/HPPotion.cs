using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : MonoBehaviour, IConsumable
{
    public List<BaseStat> Stats { get; set; }
    public void Consume()
    {
        Debug.Log("You've just consumed this potion. Nice");
        Destroy(gameObject);
    }

    public void Consume(CharacterStats stats)
    {
        Debug.Log("You've just consumed this potion. Not nice");
    }
}
