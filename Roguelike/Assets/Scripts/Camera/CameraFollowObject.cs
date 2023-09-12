using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private PlayerController _player;

    [SerializeField] private float _flipYRotationTime = 0.5f;
    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<PlayerController>();
        _isFacingRight = _player.IsFacingRight;
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
