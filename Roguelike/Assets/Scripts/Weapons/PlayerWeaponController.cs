using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    CharacterStats characterStats;
    IWeapon weaponEquipped;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);

        weaponEquipped = EquippedWeapon.GetComponent<IWeapon>();
        weaponEquipped.Stats = itemToEquip.Stats;

        EquippedWeapon.transform.SetParent(playerHand.transform);

        characterStats.AddStatBonus(itemToEquip.Stats);
        Debug.Log(weaponEquipped.Stats[0].GetCalculatedStatValue());

    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PerformWeaponAttack();
        }

        if (Input.GetButtonDown("Skill"))
        {
            PerformWeaponSkillAttack();
        }

        if (Input.GetButtonDown("Ult"))
        {
            PerformWeaponUltAttack();
        }
    }

    public void PerformWeaponAttack()
    {
        weaponEquipped.PerformAttack();
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
