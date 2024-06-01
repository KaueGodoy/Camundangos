using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerBaseStats _playerBaseStats;
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private CharacterPanel _characterPanel;

    public GameObject EquippedWeapon { get; set; }
    public CharacterStats CharacterStats { get; set; }

    Item currentlyEquippedItem;
    IWeapon weaponEquipped;

    private void Start()
    {
        //_projectileSpawn = transform.Find("ProjectileSpawn");
        CharacterStats = _playerBaseStats.CharacterStats;

        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        PerformWeaponAttack();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
            UnequipWeapon();

        EquippedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
       _playerHand.transform.position, _playerHand.transform.rotation);

        weaponEquipped = EquippedWeapon.GetComponent<IWeapon>();

        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = _projectileSpawn;
        }

        EquippedWeapon.transform.SetParent(_playerHand.transform);

        weaponEquipped.Stats = itemToEquip.Stats;
        currentlyEquippedItem = itemToEquip;

        CharacterStats.AddStatBonus(itemToEquip.Stats);

        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();

        Debug.Log(weaponEquipped.Stats[0].GetCalculatedStatValue());

    }

    public void UnequipWeapon()
    {
        if (EquippedWeapon != null)
        {
            InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
            CharacterStats.RemoveStatBonus(weaponEquipped.Stats);
            _characterPanel.UnequipWeapon();
            UIEventHandler.StatsChanged();
            Destroy(EquippedWeapon.transform.gameObject);
        }
    }

    public void PerformWeaponAttack()
    {
        if (EquippedWeapon != null)
        {
            weaponEquipped.PerformAttack(CalculateDamage());
        }
    }

    private float CalculateDamage()
    {
        float baseDamage = (CharacterStats.GetStat(BaseStat.BaseStatType.Attack).GetCalculatedStatValue())
                             * (1 + (CharacterStats.GetStat(BaseStat.BaseStatType.AttackBonus).GetCalculatedStatValue() / 100))
                             + (CharacterStats.GetStat(BaseStat.BaseStatType.FlatAttack).GetCalculatedStatValue());

        float damageToDeal = baseDamage * (1 + CharacterStats.GetStat(BaseStat.BaseStatType.DamageBonus).GetCalculatedStatValue() / 100);

        damageToDeal += CalculateCrit(damageToDeal);
        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private float CalculateCrit(float damage)
    {
        if (Random.value <= (CharacterStats.GetStat(BaseStat.BaseStatType.CritRate).GetCalculatedStatValue() / 100))
        {
            float critDamage = (damage * ((CharacterStats.GetStat(BaseStat.BaseStatType.CritDamage).GetCalculatedStatValue()) / 100));
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

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
    }
}
