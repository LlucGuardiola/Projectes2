using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [HideInInspector] public bool IsGroundDashing;
    private bool canDash;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (GetComponent<CollisionDetection>().IsTouchingFront)
        {
            EndDash();
            return;
        }

        if (IsGroundDashing)
        {
            Vector2 direction = GetComponent<PlayerMovement>().LookingForward ? Vector2.right : Vector2.left;
            rb.linearVelocity = direction * dashSpeed;
        }
    }

    public void OnGroundDash()
    {
        if (!canDash) return;
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

        animator.SetBool("IsDashing", true);

        GetComponent<PlayerMovement>().BlockHorizontalMovement(dashDuration);

        GetComponent<PlayerMovement>().Dust.Play();
    }
    private void EndDash()
    {
        canDash = false;

        IsGroundDashing = false;

        animator.SetBool("IsDashing", false);

        Invoke("EnableDashing", dashCooldown);

        GetComponent<PlayerMovement>().EnableMovement();
    }
    private void EnableDashing()
    {
        canDash = true;
    }
}
