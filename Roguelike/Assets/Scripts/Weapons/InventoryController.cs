using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController playerWeaponController;
    ConsumableController consumableController;

    public Item sword;
    public Item staff;

    public Item logPotion;

    private void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();

        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat("ATK", "Equipped sword", 6));
        sword = new Item(swordStats, "sword");

        List<BaseStat> staffStats = new List<BaseStat>();
        staffStats.Add(new BaseStat("ATK", "Equipped staff", 2));
        staff = new Item(staffStats, "staff");


        logPotion = new Item(new List<BaseStat>(), "logPotion", "Drink this to log", "Drink", "Log Potion", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerWeaponController.EquipWeapon(sword);
            consumableController.ConsumeItem(logPotion);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerWeaponController.EquipWeapon(staff);
        }

    }
}
