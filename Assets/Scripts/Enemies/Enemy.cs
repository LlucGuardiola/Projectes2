using UnityEngine;

public class Enemy : MonoBehaviour
{
 // [SerializeField] private float detectionRange = 5f;
 // [SerializeField] private float visionAngle = 90f;
    [SerializeField] public LayerMask WhatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;
  
    // jjj
    public static bool PlayerHasSword = false;

    [SerializeField] public float ChaseSpeed;
    [SerializeField] public float PatrolSpeed;

    [HideInInspector] public bool IsChasing;
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
        GetComponent<Enemy>().LookingForward = !GetComponent<Enemy>().LookingForward;
    }

    private void Update()
    {
        if (InRange)
        {
            animator.SetBool("isChasing?", false);
        }
        else if (IsChasing)
        {
            animator.SetBool("isChasing?", true);
        }
        else if (IsPatrolling && !PatrollingDisabled)
        {
            animator.SetBool("isChasing?", true);
        }
    }
}
