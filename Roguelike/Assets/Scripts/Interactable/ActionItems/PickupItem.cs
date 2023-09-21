using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item ItemDrop { get; set; }

    // override and inherits from interactable

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InitiateInteraction();
        }
    }

    public override void InitiateInteraction()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        Debug.Log(ItemDrop.ItemName);
        Debug.Log("Interacting with item object");
        Destroy(gameObject);
    }
}
