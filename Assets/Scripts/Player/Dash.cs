using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [HideInInspector] public bool IsDashing;

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject target;
    private Vector2 direction;

    public static Action<float, bool, Vector2> OnDashEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsDashing) animator.SetBool("IsDashing", true);
    }

    private void OnEnable()
    {
        SpecialAbility.OnDash += DashToTarget;
    }

    private void OnDisable()
    {
        SpecialAbility.OnDash -= DashToTarget;
    }

    private void DashToTarget(GameObject target)
    {
        if (!IsDashing)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                GetComponent<PlayerMovement>().LookingForward = !GetComponent<PlayerMovement>().LookingForward;
            }

            this.target = target;
            IsDashing = true;
            direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        }
    }

    private bool CheckDashEnd()
    {
        float playerRange = GetComponent<PlayerAttack>().AttackRange + GetComponent<PlayerAttack>().AttackSize.x;

        if (Vector2.Distance((Vector2)transform.position, (Vector2)target.transform.position) <= playerRange)
        {
            rb.linearVelocity = Vector2.zero;

            GetComponent<BoxCollider2D>().enabled = true;

            OnDashEnd?.Invoke(3f, true, direction);
            animator.SetBool("IsDashing", false);

            return false; // Dash has to end
        }

        return true;
    }

    private void FixedUpdate()
    {
        if (!IsDashing) return;
        rb.gravityScale = 0;

        if (GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //if (transform.localScale.x < 0)
        //{
        //    angle += 180;
        //}

        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.linearVelocity = direction * dashSpeed;

        IsDashing = CheckDashEnd();
    }
}
