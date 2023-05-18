using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI bossName;

    private Color stageWaitingHealthColor;
    private Color stageOneHealthColor;
    private Color stageTwoHealthColor;
    private Color stageThreeHealthColor;

    private float target = 1f;

    public void UpdateHealthBar(float maxHealth, float currentHealth, string bossName)
    {
        target = currentHealth / maxHealth;
        hpText.text = (int)currentHealth + " / " + (int)maxHealth;
        this.bossName.text = bossName;

        if (currentHealth <= 0)
        {
            hpText.text = 0.ToString() + " / " + maxHealth;
        }

        ChangeColor();
    }

    private void ChangeColor()
    {
        stageWaitingHealthColor = new Color(241f / 255f, 90f / 255f, 89f / 255f);
        stageOneHealthColor = new Color(237f / 255f, 43f / 255f, 42f / 255f);
        stageTwoHealthColor = new Color(210f / 255f, 19f / 255f, 18f / 255f);
        stageThreeHealthColor = new Color(7f / 255f, 10f / 255f, 82f / 255f);

        if (target != 0)
        {
            if (target == 1)
            {
                healthBarSprite.color = stageWaitingHealthColor;
            }
            if (target <= 0.7)
            {
                healthBarSprite.color = stageOneHealthColor;
            }
            if (target <= 0.5)
            {
                healthBarSprite.color = stageTwoHealthColor;
            }
            if (target <= 0.3)
            {
                healthBarSprite.color = stageThreeHealthColor;
            }

        }

    }

    private void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

}
