using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, IConsumable
{
    public List<BaseStat> Stats { get; set; }

    public virtual void Consume()
    {
        Debug.Log($"{gameObject.name} consumed");
        Destroy(gameObject);
    }

    public virtual void Consume(CharacterStats stats)
    {
        Debug.Log($"{gameObject.name} with stats consumed");
        Destroy(gameObject);
    }

}
