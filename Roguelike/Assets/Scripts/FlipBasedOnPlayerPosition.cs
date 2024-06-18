using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBasedOnPlayerPosition : MonoBehaviour
{
    private NewPlayerController _player;
    private Transform _target;

    [SerializeField] private bool _isFacingRight;

    private Vector2 direction;

    private void Start()
    {
        _player = NewPlayerController.Instance;
        _target = _player.transform;
    }

    private void Update()
    {
        direction = (_target.position - transform.position).normalized;
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_isFacingRight && direction.x < 0f || !_isFacingRight && direction.x > 0f)
        {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
