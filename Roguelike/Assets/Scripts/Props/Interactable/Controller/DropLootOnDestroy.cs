using System.Collections.Generic;
using UnityEngine;

public class DropLootOnDestroy : MonoBehaviour
{
    [Header("Drop")]
    public Item_HandlePickUp pickupItem;
    public DropTable DropTable { get; set; }

    private void Awake()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("coin", 100),
        };
    }

    private void DropLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            Item_HandlePickUp instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }

    private void OnDestroy()
    {
        DropLoot();
    }
}
