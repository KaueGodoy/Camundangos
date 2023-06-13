using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    //[Header("Health")]
    [SerializeField] private Player player;
    //[SerializeField] private TextMeshProUGUI health;
    //[SerializeField] private Image healthFill;

    //[Header("Level")]
    //[SerializeField] private TextMeshProUGUI level;
    //[SerializeField] private Image levelFill;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI playerStatPrefab;
    [SerializeField] private Transform playerStatPanel;
    private List<TextMeshProUGUI> playerStatTexts = new List<TextMeshProUGUI>();

    [Header("Weapon")]
    [SerializeField] private Sprite defaultWeaponSprite;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Transform weaponStatPanel;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI weaponStatPrefab;

    private PlayerWeaponController PlayerWeaponController;
    private List<TextMeshProUGUI> weaponStatTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        PlayerWeaponController = player.GetComponent<PlayerWeaponController>();
        //UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        InitializeStats();
    }

    //private void UpdateHealth(float currentHealth, float maxHealth)
    //{
    //    this.health.text = currentHealth.ToString();
    //    this.healthFill.fillAmount = currentHealth / maxHealth;
    //}

    private void InitializeStats()
    {
        Debug.Log("Stats init");
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab));
            playerStatTexts[i].transform.SetParent(playerStatPanel);
        }

        UpdateStats();
    }

    private void UpdateStats()
    {
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts[i].text = player.characterStats.stats[i].StatName + ": " +
                player.characterStats.stats[i].GetCalculatedStatValue().ToString();
        }
    }

    private void UpdateEquippedWeapon(Item item)
    {
        weaponIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
        weaponNameText.text = item.ItemName;

        for (int i = 0; i < item.Stats.Count; i++)
        {
            weaponStatTexts.Add(Instantiate(weaponStatPrefab));
            weaponStatTexts[i].transform.SetParent(weaponStatPanel);
            weaponStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
    }

    public void UnequipWeapon()
    {
        if (PlayerWeaponController.EquippedWeapon != null)
        {
            weaponNameText.text = " ";
            weaponIcon.sprite = defaultWeaponSprite;

            for (int i = 0; i < weaponStatTexts.Count; i++)
            {
                Destroy(weaponStatTexts[i].gameObject);
            }
            weaponStatTexts.Clear();
            PlayerWeaponController.UnequipWeapon();
        }
    }

}
