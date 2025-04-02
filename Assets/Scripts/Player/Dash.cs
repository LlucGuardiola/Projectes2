using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashCooldown; 
    [SerializeField] private float dashSpeed;
    private Rigidbody2D _rigidbody;
    private bool isDashing;
    private Vector2 currentTarget;
    private Vector2 nextTarget;
    private Vector2 direction;
    private float initialGravity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        initialGravity = _rigidbody.gravityScale;
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
            currentTarget = target;
            isDashing = true;
            direction = (target - (Vector2)transform.position).normalized;
        }
        else if (isDashing) nextTarget = target;
    }

    private bool CheckDashEnd()
    {
        if (((Vector2)transform.position - currentTarget).sqrMagnitude < 1f)
        {
            GetComponent<PlayerMovement>().CanMove = true;
            _rigidbody.linearVelocity = Vector2.zero;

            if (nextTarget != Vector2.positiveInfinity)
            {
                currentTarget = nextTarget;
                nextTarget = Vector2.positiveInfinity;

                //direction = (currentTarget - (Vector2)transform.position).normalized;
                isDashing = true;
            }

            _rigidbody.gravityScale = initialGravity;
            transform.rotation = Quaternion.identity;
            GetComponent<BoxCollider2D>().enabled = true;
            return false;
        }

        return true;
    }

    private void FixedUpdate()
    {
        if (!isDashing) return;
        _rigidbody.gravityScale = 0;

        if(GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Animator>().SetBool("IsOnAir?", isDashing);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, angle);

        _rigidbody.linearVelocity = direction * dashSpeed;

        isDashing = CheckDashEnd();
    }
}
