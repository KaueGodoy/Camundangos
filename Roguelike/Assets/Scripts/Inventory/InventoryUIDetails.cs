using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDetails : MonoBehaviour
{
    Item item;
    Button selectedItemButton, itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText;

    private void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();

        itemInteractButton = transform.Find("Use_Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetItem(Item item, Button selectedButton)
    {
        itemInteractButton.onClick.RemoveAllListeners();

        this.item = item;
        selectedItemButton = selectedButton;

        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        itemInteractButtonText.text = item.ActionName;
        itemInteractButton.onClick.AddListener(OnInteractItem);
    }

    public void OnInteractItem()
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }
    }
}
