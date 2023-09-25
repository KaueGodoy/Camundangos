using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ItemDropLoot
{
    public string name;
    public int number;
}

public class Item_HandleDropLoot : MonoBehaviour
{
    [SerializeField]
    private List<ItemDropLoot> itemList = new List<ItemDropLoot>();

    public DropTable DropTable { get; set; }

    private void Start()
    {

        foreach (var item in itemList)
        {
            DropTable.loot.Add(new LootDrop(item.name, item.number));
        }
    }

    public void AddItem(string name, int number)
    {
        ItemDropLoot newItem = new ItemDropLoot
        {
            name = name,
            number = number
        };
        itemList.Add(newItem);

    }

    public Item_HandlePickUp pickupItem;

    public void DropLoot()
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
