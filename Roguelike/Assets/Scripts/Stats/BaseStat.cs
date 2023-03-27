using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class BaseStat
{
    public enum BaseStatType { HP, Attack, AttackPercent, CritDamage, Defense, AttackSpeed }

    public PlayerDamage damage;
    public List<StatBonus> BaseAdditives { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public BaseStatType StatType { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float BaseValue { get; set; }
    public float FinalValue { get; set; }

    // enum > json > constructor (overload) > enemy stats > get stats > player stats

    public BaseStat(string statName, string statDescription, float baseValue)
    {
        BaseAdditives = new List<StatBonus>();
        this.StatName = statName;
        this.StatDescription = statDescription;
        this.BaseValue = baseValue;
    }

    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(BaseStatType statType, float baseValue, string statName)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.StatName = statName;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public float GetCalculatedStatValue()
    {
        this.FinalValue = 0f;
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }


}
