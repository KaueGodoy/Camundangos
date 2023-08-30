using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Dash

    [Header("Dash")]
    public float dashSpeed = 5f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    public bool canDash = true;
    public bool dashRequest = false;
    public bool isDashing;

    private Rigidbody2D _rb;

    [SerializeField] private TrailRenderer tr;

    private AudioManager _audioManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //IsJumpingMidAir = false;

        //// NEEDS IMPROVEMENT
        //if (!IsGrounded() && jumpCounter != 0)
        //{
        //    IsJumpingMidAir = true;
        //    jumpCounter--;
        //}

        _audioManager.PlaySound("PlayerDash");

        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        _rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        _rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void DashTrigger()
    {
        if (dashRequest)
        {
            StartCoroutine(Dash());
            dashRequest = false;
        }
    }
    #endregion
}
