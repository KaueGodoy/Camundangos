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

    public TextMeshProUGUI statText;

    [SerializeField] private Button _equippableTabButton;


    private void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();

        itemInteractButton = transform.Find("Use_Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);

    }

    public void SetItem(Item item, Button selectedButton)
    {
        gameObject.SetActive(true);
        itemInteractButton.Select();
        statText.text = "";
        if(item.Stats != null)
        {
            foreach(BaseStat stat in item.Stats)
            {
                statText.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }

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
            _equippableTabButton.Select();
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            _equippableTabButton.Select();
            Destroy(selectedItemButton.gameObject);
        }

        RemoveItem();
    }

    public void RemoveItem()
    {
        item = null;
        gameObject.SetActive(false);
    }
}
