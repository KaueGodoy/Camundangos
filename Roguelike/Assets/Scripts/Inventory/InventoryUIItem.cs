using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIItem : MonoBehaviour
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        SetupItemValues();
    }

    void SetupItemValues()
    {
        //this.transform.Find("Item_Name").GetComponent<Text>().text = item.ItemName;
    }
}
