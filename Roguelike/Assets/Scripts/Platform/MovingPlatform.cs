using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 2f; 
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _yOffset = 0f;

    [Header("Direction")]
    [SerializeField] private bool _moveVertically = false; 
    [SerializeField] private bool _moveDiagonally = false;

    private Vector3 _startPosition;
    private float _sinValue;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        _sinValue += Time.deltaTime * _speed;

        float x = _startPosition.x + _distance * Mathf.Sin(_sinValue);
        float y = _startPosition.y + _yOffset;
        float z = _startPosition.z;

        Vector3 newPos = new Vector3(x, y, z);

        if (_moveVertically)
        {
            newPos.x = _startPosition.x;
            newPos.y = _startPosition.y + _distance * Mathf.Sin(_sinValue);
        }

        if (_moveDiagonally)
        {
            newPos.x = _startPosition.x + _distance * Mathf.Sin(_sinValue);
            newPos.y = _startPosition.y + _distance * Mathf.Sin(_sinValue);
        }

        transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_distance, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(_distance, 0, 0));

        if (_moveVertically)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, _distance, 0));
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, _distance, 0));
        }
    }
}
