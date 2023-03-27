using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    List<BaseStat> Stats { get; set; }
    void Consume();
    void Consume(CharacterStats stats);
}

