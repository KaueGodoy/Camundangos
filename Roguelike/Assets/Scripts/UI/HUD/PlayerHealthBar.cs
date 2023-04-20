using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    [SerializeField] private TextMeshProUGUI hpText;

    private Color healthColor;
    private float target = 1f;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        hpText.text = currentHealth + " / " + maxHealth;

        if(currentHealth <= 0)
        {
            hpText.text = 0.ToString() + " / " + maxHealth;
        }

        // dynamic color change
        //healthColor = Color.Lerp(Color.red, Color.green, target);
        //healthBarSprite.color = healthColor;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (target == 1)
        {
            healthBarSprite.color = Color.cyan;
        }
        else if (target >= 0.6)
        {
            healthBarSprite.color = Color.green;
        }
        else if (target >= 0.4)
        {
            healthBarSprite.color = Color.yellow;
        }
        else if (target >= 0.2)
        {
            healthBarSprite.color = Color.red;
        }
        
    }

    private void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

}
