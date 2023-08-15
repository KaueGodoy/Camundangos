using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //private bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class");
    }

    //private void Update()
    //{
    //    Debug.Log("Condition: " + hasInteracted);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!hasInteracted)
    //    {
    //        Interact();
    //        hasInteracted = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    hasInteracted = false;
    //}

}
