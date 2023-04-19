using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IConsumable
{
    public List<BaseStat> Stats { get; set; }
    public void Consume()
    {
        Debug.Log("Coin used");
        Destroy(gameObject);
    }

    public void Consume(CharacterStats stats)
    {
        Debug.Log("Coin consumed");
    }
}
