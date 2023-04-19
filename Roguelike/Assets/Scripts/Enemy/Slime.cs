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

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Damage")]
    public float damage = 5f;
    public float attackRange = 2f;

    [Header("Aggro")]
    public float aggroRange = 20f;
    public Player player;
    public LayerMask aggroLayerMask;
    private Collider[] withinAggroColliders;

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
        characterStats = new CharacterStats(200, 10, 15, 15, 15, 25, 300, 5, 2);
        currentHealth = maxHealth;

        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 1),
            new LootDrop("staff", 1),
            new LootDrop("coin", 100),
            new LootDrop("potion_log", 50)
        };


        healthSystem = new HealthSystem(maxHealth);

        healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, transform.rotation, transform);
        healthBarTransform.gameObject.SetActive(false);

        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        Debug.Log("Health: " + healthSystem.GetCurrentHealth());
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < attackRange)
        {
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .3f, .5f);
            }
        }
        else if (distance < aggroRange)
        {
            ChasePlayer();
            CancelInvoke("PerformAttack");
            //Debug.Log("Player found! " + distance);
        }
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }


    public void PerformAttack()
    {
        player.TakeDamage(damage);
        DamagePopup.Create(transform.position, (int)damage, isCritical);

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSystem.Damage(damage);
        healthBarTransform.gameObject.SetActive(true);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DropLoot();
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
