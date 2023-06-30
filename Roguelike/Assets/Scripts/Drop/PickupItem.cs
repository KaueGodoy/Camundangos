using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item ItemDrop { get; set; }

    // override and inherits from interactable

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        Debug.Log(ItemDrop.ItemName);
        Debug.Log("Interacting with item object");
        Destroy(gameObject);
    }
}
