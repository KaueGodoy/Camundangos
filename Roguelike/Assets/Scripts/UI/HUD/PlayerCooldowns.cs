using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerCooldowns : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image foregroundImage;

    public float cooldownTimer;
    public float cooldownValue = 5.00f;
    public bool triggerCooldown;

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

            foregroundImage.gameObject.SetActive(false);
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
        triggerCooldown = false;
        cooldownTimer = cooldownValue;
        cooldownText.gameObject.SetActive(false);
        foregroundImage.gameObject.SetActive(true);
    }

}
