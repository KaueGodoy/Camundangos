using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class BaseStat
{
    public PlayerDamage damage;
    public List<StatBonus> BaseAdditives { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float BaseValue { get; set; }
    public float FinalValue { get; set; }

    public BaseStat(string statName, string statDescription, float baseValue)
    {
        BaseAdditives = new List<StatBonus>();
        this.StatName = statName;
        this.StatDescription = statDescription;
        this.BaseValue = baseValue;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(statBonus);
    }

    public float GetCalculatedStatValue()
    {
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }


}
