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

    private void Update()
    {
        if (!GetComponent<PlayerJump>().IsTouchingGround) return;

        if (IsGroundDashing)
        {
            Vector2 direction = GetComponent<PlayerMovement>().LookingForward ? Vector2.right : Vector2.left;
            rb.linearVelocity = direction * dashSpeed;
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
        Invoke("EndDash", dashDuration);
    }
    private void EndDash()
    {
        IsGroundDashing = false;
    }
}
