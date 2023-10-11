using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //float vertical = TestPlayerInput.Vertical;
        //float moveSpeed = 100f;

        //_rb.AddForce();
    }
}
