using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    private readonly float _healthThreshold = 0.0f;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            if (_currentHealth <= HealthThreshold)
            {
                Die();
            }
        }
    }

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float HealthThreshold { get { return _healthThreshold; } private set { } }

    private void Awake()
    {
        CreateLoot();
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        DamagePopup.Create(transform.position, (int)amount);
    }

    [Header("Death")]
    [SerializeField] private float _deathAnimationTime = 0.55f;
    [SerializeField] private GameObject _deathEffect;

    private void DeathEffect()
    {
        if (_deathEffect != null)
        {
            this.gameObject.SetActive(false);
            GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
        }
    }

    [Header("Drop")]
    [SerializeField] private Item_HandlePickUp _pickupItem;
    public DropTable DropTable { get; set; }

    [SerializeField] private List<string> drops = new List<string>();


    public List<string> Drops
    {
        get { return drops; }
        set { drops = value; }
    }

    private void CreateLoot()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 100),
        };
    }

    private void DropLoot()
    {
        if (Drops == null) return;

        foreach (var item in Drops)
        {
            InventoryController.Instance.GiveItem(item);
        }
    }

    private void InstantiateLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            Item_HandlePickUp instance = Instantiate(_pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }

    public virtual void Die()
    {
        DropLoot();
        Invoke("DeathEffect", _deathAnimationTime);

        Destroy(gameObject, _deathAnimationTime);
        Debug.Log("Enemy dead: " + gameObject.name);
    }
}
