using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(float hitPoints, float attack, float attackPercent, float attackFlat, float damageBonus, float critRate, float critDamage, float defense, float attackSpeed)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.HP, hitPoints, "MAX HP"),
            new BaseStat(BaseStat.BaseStatType.Attack, attack, "ATK"),
            new BaseStat(BaseStat.BaseStatType.AttackBonus, attackPercent, "ATK%"),
            new BaseStat(BaseStat.BaseStatType.FlatAttack, attackFlat, "ATK FLAT"),
            new BaseStat(BaseStat.BaseStatType.DamageBonus, damageBonus, "DMG BONUS"),
            new BaseStat(BaseStat.BaseStatType.CritRate, critRate, "CRIT RATE"),
            new BaseStat(BaseStat.BaseStatType.CritDamage, critDamage, "CRIT DAMAGE"),
            new BaseStat(BaseStat.BaseStatType.Defense, defense, "DEF"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, attackSpeed, "ATK SPD"),

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

}
