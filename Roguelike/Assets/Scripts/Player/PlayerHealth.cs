using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Awake()
    {
        _healthBar = GetComponent<PlayerHealthBar>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        IsAlive = true;
    }

    #region Health

    [Header("Health")]

    public static bool IsAlive;

    [SerializeField] private float _currentHealth = 0;
    [SerializeField] private float _maxHealth = 3;

    private PlayerHealthBar _healthBar;

    public float MaxHealth { get { return _maxHealth; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    public void UpdatePlayerHealthBar()
    {
        _healthBar.UpdateHealthBar(MaxHealth, CurrentHealth);
    }

    [Header("Hit")]

    public float HitCooldown = 0.3f;
    public float HitTimer = 0.0f;
    public static bool IsHit = false;
    private readonly float deathAnimationTime = 0.8f;

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Player takes: {damageAmount} damage");

        _audioManager.PlaySound("Hit");
        CurrentHealth -= Mathf.FloorToInt(damageAmount);
        IsHit = true;

        //UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);

        if (CurrentHealth <= 0)
        {
            UpdatePlayerHealthBar();
            Die();
            //UIEventHandler.HealthChanged(0, this.maxHealth);
            Invoke("RestartLevel", deathAnimationTime);
        }
    }
    public void Heal(float healAmount)
    {
        _audioManager.PlaySound("Hit");
        CurrentHealth += Mathf.FloorToInt(healAmount);

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
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

    #endregion

    #region Level

    private void RestartLevel()
    {
        IsAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        _audioManager.PlaySound("GameOver");
        IsAlive = false;
        //_rb.bodyType = RigidbodyType2D.Static;
    }
    #endregion 

}
