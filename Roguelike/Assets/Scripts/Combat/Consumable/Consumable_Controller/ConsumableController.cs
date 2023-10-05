using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    private IConsumable _itemToConsume;
    private CharacterStats _stats;

    private void Start()
    {
        _stats = GetComponent<PlayerController>().characterStats;
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/Potions/" + item.ObjectSlug));

        if (item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(_stats);

            _itemToConsume = itemToSpawn.GetComponent<IConsumable>();

            _itemToConsume.Stats = item.Stats;

            _stats.AddStatBonus(item.Stats);
            UIEventHandler.StatsChanged();
        }
        else
        {
            itemToSpawn.GetComponent<IConsumable>().Consume();
        }
    }
}
