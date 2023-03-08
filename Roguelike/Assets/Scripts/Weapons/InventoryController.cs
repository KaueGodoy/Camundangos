using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController playerWeaponController;
    public Item sword;
    public Item staff;

    private void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat("ATK", "Equipped sword", 6));
        sword = new Item(swordStats, "sword");

        List<BaseStat> staffStats = new List<BaseStat>();
        staffStats.Add(new BaseStat("ATK", "Equipped staff", 2));
        staff = new Item(staffStats, "staff");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerWeaponController.EquipWeapon(sword);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerWeaponController.EquipWeapon(staff);
        }

    }
}
