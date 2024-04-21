using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 0.5f;

    private TextMeshPro textMesh;
    public float disappearTimer = 0.5f;
    public float disappearSpeed = 3f;
    public float moveYSpeed = 3f;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {   // non-crit
            textMesh.fontSize = 20f;
            textColor = UtilsClass.GetColorFromString("ff1f00");
        }
        else
        {   // crit hit
            textMesh.fontSize = 30f;
            textColor = UtilsClass.GetColorFromString("9f00ff");
        }

        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX * 1f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f, 1) * 5f;
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        // non-crit
        textMesh.fontSize = 6f;
        textColor = UtilsClass.GetColorFromString("ff1f00");


        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX * 1f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f, 1) * 5f;
    }

    private void Update()
    {

        //transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;


        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {   // first half popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        }
        else
        {
            // second half popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // start disappearing 

            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
