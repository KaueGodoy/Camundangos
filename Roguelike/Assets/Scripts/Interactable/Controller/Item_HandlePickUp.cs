using UnityEngine;

public class Item_HandlePickUp : Interactable
{
    public Item ItemDrop { get; set; }

    public override void InitiateInteraction()
    {
        AddItemToInventory();
    }

    private void AddItemToInventory()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        Debug.Log(ItemDrop.ItemName);
        Debug.Log("Picked up: " + ItemDrop.ItemName);
        Destroy(gameObject);
    }
}
