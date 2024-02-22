using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
   [SerializeField] private float _moveSpeed = 2f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * _moveSpeed;
    }
    
}
