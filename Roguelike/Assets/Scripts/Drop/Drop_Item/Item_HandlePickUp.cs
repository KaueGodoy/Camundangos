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
        PrintItemInformation();
        Destroy(gameObject);
        AudioManager.Instance.PlaySound("OnItemPickup");
    }

    private void PrintItemInformation()
    {
        Debug.Log($"Picked up: {ItemDrop.ItemName}");
    }
}
