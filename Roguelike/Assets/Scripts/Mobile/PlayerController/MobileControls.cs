using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControls : MonoBehaviour
{
    [SerializeField]
    Player player;

    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 moveX;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Vector2 moveX = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(moveX.x, 0, 0);

        if (move != Vector3.zero)
        { 
            rb.velocity = move;
        }


    }

}
