using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthBar _healthBar;

    [Header("Health")]
    [SerializeField] private float _currentHealth = 0;
    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private ParticleSystem _onDeathParticle;

    public static bool IsAlive { get; set; }
    public float MaxHealth { get { return _maxHealth; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    [Header("Hit")]
    public float HitCooldown = 0.3f;
    public float HitTimer = 0.0f;
    public static bool IsHit = false;
    private readonly float deathAnimationTime = 1.2f;

   
    private void Start()
    {
        _currentHealth = _maxHealth;
        IsAlive = true;

        ParticleManager.Instance.HideParticle(_onDeathParticle);

    }

    public void UpdatePlayerHealthBar()
    {
        _healthBar.UpdateHealthBar(MaxHealth, CurrentHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Player takes: {damageAmount} damage");

        AudioManager.Instance.PlaySound("Hit");
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
        AudioManager.Instance.PlaySound("Hit");
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

    #region Level

    private void RestartLevel()
    {
        IsAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        ParticleManager.Instance.ExecuteParticle(_onDeathParticle);
        AudioManager.Instance.PlaySound("GameOver");

        NewPlayerMovement.Instance.IsControlLocked = true;
        IsAlive = false;
    }
    #endregion 

}
