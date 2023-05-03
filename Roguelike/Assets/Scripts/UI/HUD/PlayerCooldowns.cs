using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;

public class PlayerCooldowns : MonoBehaviour
{
    [Header("Skil")]
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

    // ult
    [Header("Ult")]
    [SerializeField] private TextMeshProUGUI ultCooldownText;
    [SerializeField] private Image ultForegroundImage;
    [SerializeField] private Image ultIconImage;

    public float ultCooldownTimer;
    public float ultCooldownValue = 10.0f;
    public bool ultTriggerCooldown;

    public float ultTarget;
    public float ultReduceSpeed = 2f;

    Color ultCooldownColor = new Color32(164, 164, 164, 255);
    //Color ultDefaultColor = new Color32(255, 255, 255, 168);
    Color ultDefaultColor = new Color32(255, 150, 150, 168);




    private void Start()
    {
        ResetCooldown();
        UltResetCooldown();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Skill"))
        {
            triggerCooldown = true;
        }


        if (Input.GetButtonDown("Ult"))
        {
            ultTriggerCooldown = true;
        }

    }

    private void FixedUpdate()
    {
        TriggerCooldown();
        UltTriggerCooldown();
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

    public void UltTriggerCooldown()
    {
        if (ultTriggerCooldown)
        {
            ultCooldownTimer -= Time.deltaTime;

            int numberOfDecimals = 1;

            float convertedCooldownTimer = Mathf.Round(ultCooldownTimer * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            ultIconImage.color = ultCooldownColor;

            //iconImage.gameObject.SetActive(false);
            //foregroundImage.gameObject.SetActive(false);
            ultForegroundImage.fillAmount = ultCooldownTimer / ultCooldownValue;

            ultCooldownText.gameObject.SetActive(true);
            ultCooldownText.text = convertedCooldownTimer + "";

            if (ultCooldownTimer <= 0)
            {
                UltResetCooldown();
            }
        }
    }

    public void UltResetCooldown()
    {
        ultIconImage.color = ultDefaultColor;
        ultIconImage.gameObject.SetActive(true);
        ultTriggerCooldown = false;
        ultCooldownTimer = ultCooldownValue;
        ultTarget = ultCooldownTimer / ultCooldownValue;
        ultCooldownText.gameObject.SetActive(false);
        ultForegroundImage.gameObject.SetActive(true);
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
