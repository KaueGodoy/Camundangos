using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    Transform spawnProjectile;
    CharacterStats characterStats;
    Item currentlyEquippedItem;
    IWeapon weaponEquipped;

    private void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().characterStats;
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
            characterStats.RemoveStatBonus(weaponEquipped.Stats);
            Destroy(EquippedWeapon.transform.gameObject);
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);

        weaponEquipped = EquippedWeapon.GetComponent<IWeapon>();

        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        EquippedWeapon.transform.SetParent(playerHand.transform);

        weaponEquipped.Stats = itemToEquip.Stats;
        currentlyEquippedItem = itemToEquip;

        characterStats.AddStatBonus(itemToEquip.Stats);

        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();

        Debug.Log(weaponEquipped.Stats[0].GetCalculatedStatValue());

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (EquippedWeapon != null)
            {
                PerformWeaponAttack();

            }
        }
        /*
        if (Input.GetButtonDown("Skill"))
        {
            PerformWeaponSkillAttack();
        }

        if (Input.GetButtonDown("Ult"))
        {
            PerformWeaponUltAttack();
        }*/
    }

    public void PerformWeaponAttack()
    {
        weaponEquipped.PerformAttack(CalculateDamage());
    }

    private float CalculateDamage()
    {
        float damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Attack).GetCalculatedStatValue());


        damageToDeal += CalculateCrit(damageToDeal);
        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private float CalculateCrit(float damage)
    {
        if (Random.value <= 0.5f)
        {
            float critDamage = (damage * .50f);
            return critDamage;
        }
        return 0;
    }

    public void PerformWeaponSkillAttack()
    {
        weaponEquipped.PerformSkillAttack();
    }

    public void PerformWeaponUltAttack()
    {
        weaponEquipped.PerformUltAttack();
    }

}
