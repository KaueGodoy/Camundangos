using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/Potions" + item.ObjectSlug));

        if (item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(stats);
        }
        else
        {
            itemToSpawn.GetComponent<IConsumable>().Consume();
        }

    }
}
