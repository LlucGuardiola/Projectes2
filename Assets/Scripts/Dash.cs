using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashCooldown; 
    [SerializeField] private float dashSpeed;
    private Rigidbody2D _rigidbody;
    private bool isDashing;
    private Vector2 currentTarget;
    private Vector2 nextTarget;
    private Vector2 direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
                isDashing = true;
            }

            return false;
        }

        return true;
    }

    private void FixedUpdate()
    {
        if (!isDashing) return;

        _rigidbody.linearVelocity = direction * dashSpeed;

        Debug.Log(currentTarget);

        isDashing = CheckDashEnd();
    }
}
