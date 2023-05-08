using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControls : MonoBehaviour
{
    [SerializeField]
    Player player;

    [Header("Movement")]
    public float moveSpeed = 5f;

    private Vector2 moveH;
    private Vector2 direction;
    private Vector2 moveX;

    [Header("Jump")]
    public float jumpForce = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        // Player
        if (playerInput.actions["Jump"].triggered)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (playerInput.actions["Attack"].triggered)
        {
            Debug.Log("Attack triggered");
        }

        if (playerInput.actions["Skill"].triggered)
        {
            Debug.Log("Skill triggered");
        }

        if (playerInput.actions["Ult"].triggered)
        {
            Debug.Log("Ult triggered");
        }

        if (playerInput.actions["Dash"].triggered)
        {
            Debug.Log("Dash triggered");
        }

        // UI
        /*
        if (playerInput.actions["Pause"].triggered)
        {
            Debug.Log("Pause triggered");
        }

        if (playerInput.actions["Inventory"].triggered)
        {
            Debug.Log("Inventory triggered");
        }

        if (playerInput.actions["Stats"].triggered)
        {
            Debug.Log("Stats triggered");
        }
        */

    }

    private void Move()
    {
        moveH = playerInput.actions["Move"].ReadValue<Vector2>();

        direction = new Vector2(moveH.x * moveSpeed, rb.velocity.y);

        if (direction != Vector2.zero)
        {
            rb.velocity = direction;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

}
