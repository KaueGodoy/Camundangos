using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 TeleportLocation { get; set; }
    [SerializeField]
    private Portal[] linkedPortals;
    private PortalController PortalController { get; set; }

    private void Start()
    {
        PortalController = FindObjectOfType<PortalController>();
        TeleportLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");

            Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DeactivateInteraction();
    }

    public void Interact()
    {
        PortalController.ActivatePortal(linkedPortals);
    }

    public void DeactivateInteraction()
    {
        PortalController.DeactivatePortal();
    }
}
