using UnityEngine;

public class EnemyHealthBarHandler : MonoBehaviour
{
    public Transform pfHealthBar;

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(200f);

        Transform healthBarTransform = Instantiate(pfHealthBar, transform.position + new Vector3(0, 10), transform.rotation);
        EnemyHealthBar healthBar = healthBarTransform.GetComponent<EnemyHealthBar>();

        healthBar.Setup(healthSystem);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
    }
}
