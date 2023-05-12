using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    private PlayerControls playerControls;

    [SerializeField] private GameObject downButton;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private float collisionDisableTime = 0.5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {

        downButton.SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        if (playerControls.Player.Down.triggered)
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
            downButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
            downButton.SetActive(false);
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(collisionDisableTime);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

    }

}
