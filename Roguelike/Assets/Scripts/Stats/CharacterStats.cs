using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public List<BaseStat> stats = new List<BaseStat>();

    private void Start()
    {
        // Basic Stats
        stats.Add(new BaseStat("Attack", "Character's attack", 67f));
        stats.Add(new BaseStat("Defense", "Character's defense", 127f));

        stats[0].AddStatBonus(new StatBonus(5f));
        Debug.Log(stats[0].GetCalculatedStatValue());




    }
}
