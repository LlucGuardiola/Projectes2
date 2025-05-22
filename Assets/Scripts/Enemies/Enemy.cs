using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public LayerMask WhatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;

    [SerializeField] public float ChaseSpeed;
    [SerializeField] public float PatrolSpeed;

    [HideInInspector] public bool IsChasing;
    [HideInInspector] public bool IsDead;
    [HideInInspector] public bool HasToChase;
    [HideInInspector] public bool IsAttacking;
    [HideInInspector] public bool IsPatrolling;
    [HideInInspector] public bool LookingForward;

    private Animator animator;
    public bool InRange;
    public bool MeleeAttack;
    public bool DistanceAttack;
    public bool PatrollingDisabled;

    private void Start()
    {
        LookingForward = true;
        IsPatrolling = true;
        animator = GetComponent<Animator>();
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        LookingForward = !LookingForward;
    }

    private void Update()
    {
        if (IsDead)
        {
            animator.SetBool("IsDead", true);
            animator.SetBool("isChasing?", false);
            animator.SetBool("isPatrolling?", false);
            return;
        }

        if (InRange)
        {
            animator.SetBool("isChasing?", false);
            animator.SetBool("isPatrolling?", false);
        }
        else if (IsChasing)
        {
            IsPatrolling = false;
            animator.SetBool("isPatrolling?", false);
            animator.SetBool("isChasing?", true);
        }
        else if (IsPatrolling && !PatrollingDisabled)
        {
            animator.SetBool("isPatrolling?", true);
            animator.SetBool("isChasing?", false);
        }
    }
}
