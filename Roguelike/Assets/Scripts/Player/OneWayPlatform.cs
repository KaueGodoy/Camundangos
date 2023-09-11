using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D _playerCollider;
    private BoxCollider2D _platformCollider;
    private PlayerControls _playerControls;

    private GameObject _currentOneWayPlatform;

    [SerializeField] private GameObject downButton;
    [SerializeField] private float _collisionDisableTime = 0.5f;

    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        downButton.SetActive(false);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        if (_playerControls.Player.Down.triggered)
        {
            if (_currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            _currentOneWayPlatform = collision.gameObject;
            downButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            _currentOneWayPlatform = null;
            downButton.SetActive(false);
        }
    }

    private IEnumerator DisableCollision()
    {
        _platformCollider = _currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(_playerCollider, _platformCollider);
        yield return new WaitForSeconds(_collisionDisableTime);
        Physics2D.IgnoreCollision(_playerCollider, _platformCollider, false);

    }

}
