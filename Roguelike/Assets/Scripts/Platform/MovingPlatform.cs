using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // The speed of the platform
    public float distance = 5f; // The distance the platform moves
    public bool moveVertically = false; // Toggle for vertical movement

    private Vector3 startPosition;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Calculate the new position of the platform
        Vector3 newPos = new Vector3(startPosition.x + distance * Mathf.Sin(timer * speed),
                                     startPosition.y,
                                     startPosition.z);

        if (moveVertically)
        {
            newPos.x = startPosition.x;
            newPos.y = startPosition.y + distance * Mathf.Sin(timer * speed);
        }

        // Move the platform
        transform.position = newPos;
    }

    // Draw gizmos to show the platform's movement range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(distance, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(distance, 0, 0));

        if (moveVertically)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, distance, 0));
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, distance, 0));
        }
    }
}
