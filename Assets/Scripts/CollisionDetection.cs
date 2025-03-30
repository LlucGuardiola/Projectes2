using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;

    [SerializeField]
    private Transform GroundCheckPoint;
    [SerializeField]
    private Transform FrontCheckPoint;


    [SerializeField] private float _checkRadius = 0.15f;
    [SerializeField] private float _frontRadius = 0.55f;

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    [SerializeField]
    private bool _isTouchingFront;
    public bool IsTouchingFront { get { return _isTouchingFront; } }

    [HideInInspector] public int CollisionPos;

    [SerializeField]
    private float _distanceToGround;
    public float DistanceToGround { get { return _distanceToGround; } }

    [SerializeField]
    private float _groundAngle;
    public float GroundAngle { get { return _groundAngle; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, _checkRadius);
        Gizmos.DrawWireSphere(FrontCheckPoint.position, _frontRadius);
        Gizmos.color = Color.white;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        CheckFront();
    }

    private void CheckFront()
    {
        var colliders = Physics2D.OverlapCircleAll(FrontCheckPoint.position, _frontRadius, WhatIsGround);

        _isTouchingFront = colliders.Length > 0;

        if (_isTouchingFront)
        {
            Transform collisionTransform = colliders[0].transform;

            CollisionPos = collisionTransform.position.x > transform.position.x ? 1 : -1;
        }
        else { CollisionPos = 0; }
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, _checkRadius, WhatIsGround);

        _isGrounded =  colliders.Length > 0;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
        _groundAngle = Vector2.Angle(hit.normal,new Vector2(1,0));
    }
}
