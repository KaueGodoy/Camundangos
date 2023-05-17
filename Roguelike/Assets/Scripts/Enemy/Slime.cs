using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 200;

    [Header("HP Bar")]
    public Transform pfHealthBar;
    public Vector3 offset = new Vector3(0, 1f);

    [Header("Drop")]
    public PickupItem pickupItem;
    public DropTable DropTable { get; set; }

    public int ID { get; set; }

    HealthSystem healthSystem;
    Transform healthBarTransform;

    private CharacterStats characterStats;
    private bool isCritical;

    void Start()
    {
        currentHealth = maxHealth;

        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 1),
            new LootDrop("staff", 1),
            new LootDrop("coin", 100),
            new LootDrop("potion_log", 50)
        };

    }

    public void PerformAttack()
    {
        //player.TakeDamage(damage);
        //DamagePopup.Create(transform.position, (int)damage, isCritical);

    }

    private void InstantiateHealthBar()
    {
        healthSystem = new HealthSystem(maxHealth);

        healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, Quaternion.identity, transform);

        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

    }

    public void TakeDamage(float damage)
    {
        if (currentHealth == maxHealth)
        {
            InstantiateHealthBar();
        }

        currentHealth -= damage;
        healthSystem.Damage(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DropLoot();
        healthSystem.Die();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
}
