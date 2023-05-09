using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControls : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Vector2 moveH;
    private Vector2 direction;

    [Header("Jump")]
    public float jumpForce = 5f;

    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Move();

        // Player
        if (playerControls.Player.Jump.triggered)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (playerControls.Player.Attack.triggered)
        {
            Debug.Log("Attack triggered");
        }

        if (playerControls.Player.Skill.triggered)
        {
            Debug.Log("Skill triggered");
        }

        if (playerControls.Player.Ult.triggered)
        {
            Debug.Log("Ult triggered");
        }

        if (playerControls.Player.Dash.triggered)
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
        moveH = playerControls.Player.Move.ReadValue<Vector2>();

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
