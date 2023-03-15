using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;

public class PlayerCooldowns : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image iconImage;

    public float cooldownTimer;
    public float cooldownValue = 5.00f;
    public bool triggerCooldown;

    public float target;
    public float reduceSpeed = 2f;

    Color cooldownColor = new Color32(164, 164, 164, 255);
    Color defaultColor = new Color32(255, 255, 255, 168);


    private void Start()
    {
        ResetCooldown();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Skill"))
        {
            triggerCooldown = true;
        }

    }

    private void FixedUpdate()
    {
        TriggerCooldown();

    }

    public void TriggerCooldown()
    {
        if (triggerCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            int numberOfDecimals = 1;

            float convertedCooldownTimer = Mathf.Round(cooldownTimer * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            iconImage.color = cooldownColor;

            //iconImage.gameObject.SetActive(false);
            //foregroundImage.gameObject.SetActive(false);
            foregroundImage.fillAmount = cooldownTimer / cooldownValue;

            cooldownText.gameObject.SetActive(true);
            cooldownText.text = convertedCooldownTimer + "";

            if (cooldownTimer <= 0)
            {
                ResetCooldown();
            }
        }
    }

    public void ResetCooldown()
    {
        iconImage.color = defaultColor;
        iconImage.gameObject.SetActive(true);
        triggerCooldown = false;
        cooldownTimer = cooldownValue;
        target = cooldownTimer / cooldownValue;
        cooldownText.gameObject.SetActive(false);
        foregroundImage.gameObject.SetActive(true);
    }

}
