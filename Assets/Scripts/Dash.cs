using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashCooldown; 
    [SerializeField] private float dashSpeed;
    private Rigidbody2D _rigidbody;
    private bool isDashing;
    private Vector2 target;
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
            this.target = target;
            isDashing = true;
            direction = (target - (Vector2)transform.position).normalized;
        }
    }

    private bool CheckDashEnd()
    {
        if (((Vector2)transform.position - target).sqrMagnitude < 0.5f)
        {
            GetComponent<PlayerMovement>().CanMove = true;
            return false;
        }

        return true;
    }

    private void FixedUpdate()
    {
        if (!isDashing) return;


        Vector2 _horizontalDir = direction;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir.x * dashSpeed;


        _rigidbody.linearVelocity = velocity;



        _rigidbody.linearVelocity = direction * dashSpeed;

        Debug.Log(target);

        isDashing = CheckDashEnd();
    }
}
