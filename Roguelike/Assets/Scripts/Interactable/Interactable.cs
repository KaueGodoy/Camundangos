using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        Debug.Log("Interacting with the base class");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            Interact();
        }
    }
}
