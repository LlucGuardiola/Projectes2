using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private LayerMask WhatIsGround;

    [SerializeField] private Transform GroundCheckPoint;
    [SerializeField] private Transform FrontCheckPoint;

    [SerializeField] private Vector2 _checkSize;
    [SerializeField] private Vector2 _frontSize;

    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    private bool _isTouchingFront;
    public bool IsTouchingFront { get { return _isTouchingFront; } }

    [HideInInspector] public int CollisionPos;

    private float _distanceToGround;
    [HideInInspector] public float DistanceToGround { get { return _distanceToGround; } }

    private float _groundAngle;
    public float GroundAngle { get { return _groundAngle; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundCheckPoint.position, _checkSize);
        Gizmos.DrawWireCube(FrontCheckPoint.position, _frontSize);
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
        var colliders = Physics2D.OverlapBoxAll(FrontCheckPoint.position, _frontSize, 0, WhatIsGround);

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
        var colliders = Physics2D.OverlapBoxAll(GroundCheckPoint.position, _checkSize, 0, WhatIsGround);

        _isGrounded = colliders.Length > 0;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
        _groundAngle = Vector2.Angle(hit.normal, new Vector2(1, 0));
    }
}
