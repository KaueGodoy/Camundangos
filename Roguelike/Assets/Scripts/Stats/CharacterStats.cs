using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public List<BaseStat> stats = new List<BaseStat>();
    public List<BaseStat> stats2 = new List<BaseStat>();

    public CharacterStats(float attack, float defense, float attackSpeed)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.Attack, attack, "current ATK"),
            new BaseStat(BaseStat.BaseStatType.Defense, defense, "current DEF"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, attackSpeed, "current ATK speed"),

        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach(BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }

    }
    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }

    }


    private void Start()
    {
        // Basic Stats
        stats2.Add(new BaseStat("Attack", "Character's attack", 67f));
        stats2.Add(new BaseStat("Defense", "Character's defense", 127f));

        //stats[0].AddStatBonus(new StatBonus(5f));
        //Debug.Log(stats[0].GetCalculatedStatValue());




    }
}
