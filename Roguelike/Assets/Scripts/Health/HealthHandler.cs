using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public Transform pfHealthBar;

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(200f);

        Transform healthBarTransform = Instantiate(pfHealthBar, transform.position + new Vector3(0, 10), transform.rotation);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();

        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
    }
}
