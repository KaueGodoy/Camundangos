using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpHeight = 2.0f;
    public float jumpRange = 5.0f;
    public float jumpSpeed = 5.0f;
    public float returnSpeed = 2.0f;
    public float jumpCooldown = 2.0f;

    private bool isJumping = false;
    private bool hasJumped = false;
    private float lastJumpTime = -Mathf.Infinity;

    [Header("Damage")]
    public float damage = 100f;

    private Vector3 initialPosition;
    private Transform playerTransform;


    void Start()
    {
        initialPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= jumpRange && !hasJumped && Time.time >= lastJumpTime + jumpCooldown)
        {
            hasJumped = true;
            Jump();
            lastJumpTime = Time.time;
        }

        if (isJumping)
        {
            transform.position += transform.up * jumpSpeed * Time.deltaTime;

            if (transform.position.y >= initialPosition.y + jumpHeight)
            {
                isJumping = false;
            }
        }
        else if (transform.position.y > initialPosition.y)
        {
            transform.position -= transform.up * returnSpeed * Time.deltaTime;

            if (transform.position.y <= initialPosition.y)
            {
                transform.position = initialPosition;
                hasJumped = false; // Reset the jump trigger
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        // Add code for any jump animation or sound effect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(damage);
            DamagePopup.Create(player.transform.position + Vector3.right + Vector3.up, (int)damage);
        }
    }
}
