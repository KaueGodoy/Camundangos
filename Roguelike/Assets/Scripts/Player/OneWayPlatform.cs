using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private GameObject downButton;
    [SerializeField] private float _collisionDisableTime = 0.5f;

    private BoxCollider2D _playerCollider;
    private BoxCollider2D _platformCollider;

    private GameObject _currentOneWayPlatform;

    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerDescendPlatform += GameInput_OnPlayerDescendPlatform;
        downButton.SetActive(false);
    }

    private void GameInput_OnPlayerDescendPlatform(object sender, System.EventArgs e)
    {
        DescendPlatform();
    }

    private void DescendPlatform()
    {
        if (_currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
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
