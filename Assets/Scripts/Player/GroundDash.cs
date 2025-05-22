using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [HideInInspector] public bool IsGroundDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!IsGroundDashing) return;

        Vector2 direction = GetComponent<PlayerMovement>().LookingForward ? Vector2.right : Vector2.left;
        float distance = dashSpeed * Time.fixedDeltaTime;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = rb.Cast(direction, hits, distance);

        if (hitCount == 0)
        {
            // No hi ha res davant, mou
            rb.MovePosition(rb.position + direction * distance);
        }
        else
        {
            // Topem amb alguna cosa → atura dash
            EndDash();
        }
    }

    public void OnGroundDash()
    {
        if (IsGroundDashing) return;
        if (!GetComponent<PlayerJump>().IsTouchingGround) return;
        if (GetComponent<Dash>().IsDashing) return;
        if (GetComponent<Parry>().IsParring) return;
        if (GetComponent<PlayerAttack>().isAttacking) return;

        StartDash();
    }

    private void StartDash()
    {
        IsGroundDashing = true;
        if (GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        Invoke("EndDash", dashDuration);

        GetComponent<PlayerMovement>().BlockHorizontalMovement(dashDuration);
    }
    private void EndDash()
    {
        IsGroundDashing = false;

        GetComponent<BoxCollider2D>().enabled = true;

        GetComponent<PlayerMovement>().EnableMovement();
    }
}
