using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();
        if (player != null)
        {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
