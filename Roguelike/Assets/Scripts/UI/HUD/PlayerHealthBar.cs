using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;
    [SerializeField] private float _reduceSpeed = 2f;
    [SerializeField] private TextMeshProUGUI _hpText;

    private float _target = 1f;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _target = currentHealth / maxHealth;
        _hpText.text = currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            _hpText.text = 0.ToString() + " / " + maxHealth;
        }

        ChangeColor();
    }

    private void ChangeColor()
    {
        if (_target == 1)
        {
            _healthBarSprite.color = Color.cyan;
        }
        else if (_target >= 0.6)
        {
            _healthBarSprite.color = Color.green;
        }
        else if (_target >= 0.4)
        {
            _healthBarSprite.color = Color.yellow;
        }
        else if (_target >= 0.2)
        {
            _healthBarSprite.color = Color.red;
        }
    }

    private void Update()
    {
        _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    }

}
