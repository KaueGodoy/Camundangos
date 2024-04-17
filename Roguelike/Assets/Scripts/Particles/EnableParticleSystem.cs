using UnityEngine;

public class EnableParticleSystem : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        gameObject.SetActive(true);
    }
}
