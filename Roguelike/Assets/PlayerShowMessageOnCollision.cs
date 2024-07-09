using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowMessageOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the name of the object that the player collided with
        string collidedObjectName = collision.gameObject.name;

        // Show a message with the name of the collided object
        Debug.Log("Player collided with: " + collidedObjectName);
    }
}
