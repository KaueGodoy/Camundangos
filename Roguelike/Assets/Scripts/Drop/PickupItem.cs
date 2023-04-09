using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
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

    public void Interact()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        Debug.Log(ItemDrop.ItemName);
        Destroy(gameObject);
    }
}
