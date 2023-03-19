using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CharacterPanel : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private Image healthFill;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Image levelFill;

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
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += EquipWeapon;
        InitializeStats();
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = currentHealth / maxHealth;
    }

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

    private void EquipWeapon(Item item)
    {
        Debug.Log(item.ItemName);
    }

}
