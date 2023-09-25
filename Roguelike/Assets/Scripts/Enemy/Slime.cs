using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 200;
    private readonly float healthThreshold = 0.0f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    public int ID { get; set; }

    HealthSystem healthSystem;
    Transform healthBarTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

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


        if (currentHealth <= 0)
        {
            DisableHealthBar();

            rb.bodyType = RigidbodyType2D.Static;
            boxCollider.enabled = false;


            Invoke("Die", deathAnimationTime);
            Invoke("DeathEffect", deathAnimationTime);
        }
    }

    [Header("Death")]
    [SerializeField] private float deathAnimationTime = 0.55f;
    [SerializeField] private GameObject pfDeathEffect;

    private void DeathEffect()
    {
        if (pfDeathEffect != null)
        {
            this.gameObject.SetActive(false);
            GameObject effect = Instantiate(pfDeathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathAnimationTime);
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

        EnemyHealthBar healthBar = healthBarTransform.GetComponent<EnemyHealthBar>();
        healthBar.Setup(healthSystem);

    }

    private void DisableHealthBar()
    {
        healthSystem.Die();
        healthBarTransform.gameObject.SetActive(false);
        //Destroy(healthBarTransform.gameObject);
    }

    [Header("Drop")]
    public Item_HandlePickUp pickupItem;
    public DropTable DropTable { get; set; }
    public bool IsDead { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void DropLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            Item_HandlePickUp instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
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
