using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        Destroy(gameObject);
        Debug.Log("Object destroyed: " + this.gameObject.name);
    }

}
