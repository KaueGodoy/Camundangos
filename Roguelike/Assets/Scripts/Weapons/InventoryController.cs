using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Item sword;

    private void Start()
    {
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat("ATK", "Equipped sword", 6));
        sword = new Item(swordStats, "sword");
    }
}
