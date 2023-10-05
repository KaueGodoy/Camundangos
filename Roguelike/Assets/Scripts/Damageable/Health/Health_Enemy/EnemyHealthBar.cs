using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Transform _barTransform;

    private void Awake()
    {
        _barTransform = transform.Find("Bar");
    }

    public void Setup(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateHealthBar();
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_barTransform != null)
        {
            _barTransform.localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
        }
        else
        {
            Debug.LogWarning("Bar transform not found.");
        }

    }
}
