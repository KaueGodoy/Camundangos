using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTest : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Vector2 moveH;
    private Vector2 direction;
    private Vector2 moveX;

    [Header("Jump")]
    public float jumpForce = 5f;

    private PlayerTest2 playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = new PlayerTest2();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        moveH = playerInput.PlayerTest.Move.ReadValue<Vector2>();

        direction = new Vector2(moveH.x * moveSpeed, rb.velocity.y);

        if (direction != Vector2.zero)
        {
            rb.velocity = direction;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (playerInput.PlayerTest.Jump.triggered)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

    }
}
