using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemy
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 200;
    [SerializeField] private float deathAnimationTime = 0.4f;
    private readonly float healthThreshold = 0.0f;

    public DropTable DropTable { get; set; }

    HealthSystem healthSystem;
    Transform healthBarTransform;

    public int ID { get; set; }

    private void Start()
    {
        currentHealth = maxHealth;

        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("coin", 100),
        };
    }

    private void Update()
    {
        UpdateTimers();
    }

    [Header("Timers")]
    private float hitCooldown = 0.3f;
    [HideInInspector] public bool isHit = false;

    private float hitTimer = 0.0f;

    private void UpdateTimers()
    {
        // hit
        if (isHit)
        {
            hitTimer += Time.deltaTime;
        }

        if (hitTimer > hitCooldown)
        {
            isHit = false;
            hitTimer = 0f;
        }
    }

    public void PerformAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        if (currentHealth == maxHealth)
        {
            InstantiateHealthBar();
        }

        isHit = true;

        currentHealth -= damage;
        healthSystem.Damage(damage);

        //Debug.Log("Health: " + healthSystem.GetHealthPercent());
        //Debug.Log("Health: " + healthSystem.GetCurrentHealth());

        if (currentHealth <= 0)
        {
            Invoke("Die", deathAnimationTime);
        }
    }

    public void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    [Header("HP Bar")]
    public Transform pfHealthBar;
    public Vector3 offset = new Vector3(0, 1f);

    private void InstantiateHealthBar()
    {
        healthSystem = new HealthSystem(maxHealth);

        healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, Quaternion.identity, transform);
        //healthBarTransform.gameObject.SetActive(false);

        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        Debug.Log("Health: " + healthSystem.GetCurrentHealth());
    }

    [Header("Drop")]
    public PickupItem pickupItem;

    void DropLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }

    public bool IsAlive()
    {
        return currentHealth > healthThreshold;
    }

    public bool isDead()
    {
        return currentHealth <= healthThreshold;
    }

    public bool IsDamaged()
    {
        return currentHealth < maxHealth;
    }

    public bool isFullHealth()
    {
        return currentHealth == maxHealth;
    }
}
