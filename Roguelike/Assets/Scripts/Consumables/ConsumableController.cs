using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;
    IConsumable itemToConsume;

    private void Start()
    {
        stats = GetComponent<Player>().characterStats;
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/Potions/" + item.ObjectSlug));

        if (item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(stats);

            itemToConsume = itemToSpawn.GetComponent<IConsumable>();


            itemToConsume.Stats = item.Stats;

            stats.AddStatBonus(item.Stats);
            UIEventHandler.StatsChanged();
        }
        else
        {
            itemToSpawn.GetComponent<IConsumable>().Consume();
        }


    }
}
