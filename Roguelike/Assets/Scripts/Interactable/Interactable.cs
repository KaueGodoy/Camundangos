using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    private InteractableController _interactableController;

    public virtual void Awake()
    {
        _interactableController = FindObjectOfType<InteractableController>();

        if (_interactableController == null)
            Debug.LogError("InteractableController not found in the scene.");
        else
            Debug.Log("InteractableController found in the scene.");
    }

    public virtual void InitiateInteraction()
    {
        Debug.Log("Interacting with the base class");
        _interactableController.ShowInteractionUI(gameObject.name);
    }

    public virtual void DisableInteraction()
    {
        Debug.Log("Disabling interaction");
        _interactableController.HideInteractionUI();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            InitiateInteraction();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            DisableInteraction();
        }
    }
}
