using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [HideInInspector] public bool IsGroundDashing;
    private float initialGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialGravity = rb.gravityScale;
    }

    private void Update()
    {
        if (!GetComponent<PlayerJump>().IsTouchingGround)
        {
            IsGroundDashing = false;
            return;
        }

        if (GetComponent<CollisionDetection>().IsTouchingFront)
        {
            IsGroundDashing = false;
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
        if (IsGroundDashing) return;
        Debug.Log("1");
        if (!GetComponent<PlayerJump>().IsTouchingGround) return;
        Debug.Log("2");
        if (GetComponent<Dash>().IsDashing) return;
        Debug.Log("3");
        if (GetComponent<Parry>().IsParring) return;
        Debug.Log("4");
        if (GetComponent<PlayerAttack>().isAttacking) return;
        Debug.Log("5");

        StartDash();
    }

    private void StartDash()
    {
        IsGroundDashing = true;
        rb.gravityScale = 0;
        if (GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        Invoke("EndDash", dashDuration);
    }
    private void EndDash()
    {
        IsGroundDashing = false;

        rb.gravityScale = 0;

        if (GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = true;
    }
}
