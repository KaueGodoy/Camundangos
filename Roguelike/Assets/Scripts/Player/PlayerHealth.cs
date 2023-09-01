using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Health

    private void Awake()
    {
        _healthBar = GetComponent<PlayerHealthBar>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        IsAlive = true;
    }

    [Header("Health")]
    public float currentHealth = 0;
    public float maxHealth = 3;

    [Header("Damage and Heal")]
    public float damageAmount = 1f;
    public float healAmount = 1f;

    public bool IsAlive;
    private readonly float deathAnimationTime = 0.8f;

    [Header("Hit")]
    public float HitCooldown = 0.3f;
    public float HitTimer = 0.0f;
    public bool IsHit = false;

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Player takes: {damageAmount} damage");

        FindObjectOfType<AudioManager>().PlaySound("Hit");
        currentHealth -= Mathf.FloorToInt(damageAmount);
        IsHit = true;

        //UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);

        if (currentHealth <= 0)
        {
            UpdatePlayerHealthBar();
            Die();
            //UIEventHandler.HealthChanged(0, this.maxHealth);
            Invoke("RestartLevel", deathAnimationTime);
        }
    }

    public void UpdateHitTimer()
    {
        if (IsHit)
            HitTimer += Time.deltaTime;

        if (HitTimer > HitCooldown)
        {
            IsHit = false;
            HitTimer = 0f;
        }
    }

    public void Heal(float healAmount)
    {
        FindObjectOfType<AudioManager>().PlaySound("Hit");
        currentHealth += Mathf.FloorToInt(healAmount);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    #endregion

    #region UI
    [Header("UI Elements")]

    private PlayerHealthBar _healthBar;

    public void UpdatePlayerHealthBar()
    {
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        //cooldowns.UpdateCooldowns(attackTimer);
    }

    #endregion

    #region Level

    private void RestartLevel()
    {
        IsAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
        IsAlive = false;
        //_rb.bodyType = RigidbodyType2D.Static;
    }
    #endregion 

}
