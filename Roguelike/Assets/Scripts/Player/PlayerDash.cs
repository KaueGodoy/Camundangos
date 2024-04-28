using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private TrailRenderer tr;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    private Coroutine _dashCoroutine;

    public bool CanDash { get; set; }
    public bool DashRequest = false;
    public bool IsDashing;

    public float CurrentRotation { get { return transform.rotation.y >= 0 ? 1 : -1; } set { } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        CanDash = true;
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;

        //IsJumpingMidAir = false;

        //// NEEDS IMPROVEMENT
        //if (!IsGrounded() && jumpCounter != 0)
        //{
        //    IsJumpingMidAir = true;
        //    jumpCounter--;
        //}

        AudioManager.Instance.PlaySound("PlayerDash");

        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        //_rb.velocity = new Vector2(transform.localScale.x * _dashSpeed, 0f);
        _rb.velocity = new Vector2(CurrentRotation * _dashSpeed, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        tr.emitting = false;
        _rb.gravityScale = originalGravity;
        IsDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        CanDash = true;
    }

    public void CallDashCoroutine()
    {
        _dashCoroutine = StartCoroutine(Dash());
    }

    public void TriggerDash()
    {
        if (DashRequest)
        {
            CallDashCoroutine();
            DashRequest = false;
        }
    }
}
