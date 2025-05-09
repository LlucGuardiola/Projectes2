using UnityEngine;

public class Enemy : MonoBehaviour
{
 // [SerializeField] private float detectionRange = 5f;
 // [SerializeField] private float visionAngle = 90f;
    [SerializeField] public LayerMask WhatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;

    [SerializeField] public float PatrolSpeed;
    [SerializeField] public float ChaseSpeed;

    public bool HasToChase;
    public bool IsChasing;
    public bool IsPatrolling;
    public bool LookingForward;
    public bool InRange;
    public bool DistanceAttack;
    public bool MeleeAttack;
    private Animator animator;

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
        else if (IsChasing || IsPatrolling)
        {
            animator.SetBool("isChasing?", true);
        }
    }
}
