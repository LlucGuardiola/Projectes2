using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashCooldown; 
    [SerializeField] private float dashSpeed;

    private Rigidbody2D rb;
    private bool isDashing;
    private Vector2 target;
    private Vector2 direction;
    private float initialGravity;

    public static Action<float> OnDashEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialGravity = rb.gravityScale;
    }

    private void OnEnable()
    {
        SpecialAbility.OnDash += DashToTarget;
    }

    private void OnDisable()
    {
        SpecialAbility.OnDash -= DashToTarget;
    }

    private void DashToTarget(Vector2 target)
    {
        if (!isDashing && (dashCooldown == 0))
        {
            this.target = target;
            isDashing = true;
            direction = (target - (Vector2)transform.position).normalized;
        }
    }

    private bool CheckDashEnd()
    {
        if (((Vector2)transform.position - target).sqrMagnitude < 1f)
        {
            GetComponent<PlayerMovement>().CanMove = true;
            rb.linearVelocity = Vector2.zero;

            rb.gravityScale = initialGravity;
            transform.rotation = Quaternion.identity;
            GetComponent<BoxCollider2D>().enabled = true;

            OnDashEnd?.Invoke(3f);

            return false; // Dash has to end
        }

        return true;
    }

    private void FixedUpdate()
    {
        if (!isDashing) return;
        rb.gravityScale = 0;

        if(GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Animator>().SetBool("IsOnAir?", isDashing);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.linearVelocity = direction * dashSpeed;

        isDashing = CheckDashEnd();
    }
}
