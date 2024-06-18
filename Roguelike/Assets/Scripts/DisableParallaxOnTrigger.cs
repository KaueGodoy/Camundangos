using UnityEngine;

public class DisableParallaxOnTrigger : MonoBehaviour
{
    [SerializeField] private Transform _sky;
    [SerializeField] private Transform _mountains;

    [SerializeField] private Transform _trees03;
    [SerializeField] private Transform _trees02;
    [SerializeField] private Transform _trees01;

    [SerializeField] private Transform _bush03;
    [SerializeField] private Transform _bush02;
    [SerializeField] private Transform _bush01;
    [SerializeField] private Transform _bush00;
    [SerializeField] private Transform _bush001;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerRigidbody.velocity.x > 0)
            {
                DisableForestParallax();
                Debug.Log("Disabling this parallax from the right");
            }
            else
            {
                EnableForestParallax();
                Debug.Log("Enabling this parallax from the left");
            }
        }

    }
    private void DisableParallax(Transform transform)
    {
        ParallaxBackground transformParallax = transform.gameObject.GetComponent<ParallaxBackground>();
        transformParallax.enabled = false;
    }

    private void EnableParallax(Transform transform)
    {
        ParallaxBackground transformParallax = transform.gameObject.GetComponent<ParallaxBackground>();
        transformParallax.enabled = true;
    }

    private void EnableForestParallax()
    {
        EnableParallax(_sky);
        EnableParallax(_mountains);

        EnableParallax(_trees03);
        EnableParallax(_trees02);
        EnableParallax(_trees01);

        EnableParallax(_bush03);
        EnableParallax(_bush02);
        EnableParallax(_bush01);
        EnableParallax(_bush00);
        EnableParallax(_bush001);
    }

    private void DisableForestParallax()
    {
        DisableParallax(_sky);
        DisableParallax(_mountains);

        DisableParallax(_trees03);
        DisableParallax(_trees02);
        DisableParallax(_trees01);

        DisableParallax(_bush03);
        DisableParallax(_bush02);
        DisableParallax(_bush01);
        DisableParallax(_bush00);
        DisableParallax(_bush001);
    }
}
