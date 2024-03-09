using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private NewPlayerMovement _playerMovement;
    private bool _isFacingRight;

    private void Awake()
    {
        _playerMovement = _playerTransform.GetComponent<NewPlayerMovement>();
        _isFacingRight = _playerMovement.IsFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.transform.position;
    }

    public void TurnCamera()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }

}
