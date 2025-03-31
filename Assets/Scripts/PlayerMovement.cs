using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool CanMove;

    [SerializeField] private float Speed = 5.0f;

    private float inputVal;
    private float _horizontalDir;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;

    int CollisionPos => _collisionDetection.CollisionPos;

    void Start()
    {
        CanMove = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
    }

    void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("IsRunning?", _rigidbody.linearVelocity.x != 0 && GetComponent<PlayerJump>().IsTouchingGround);

        if (!CanMove) return;

        _horizontalDir = inputVal;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;

        if ((CollisionPos == 1 && _horizontalDir > 0) || (CollisionPos == -1 && _horizontalDir < 0))
        {
            velocity.x = 0;
        }

        _rigidbody.linearVelocity = velocity;
    }

    private void Update()
    {
    }

    void OnMove(InputValue value)
    {
        inputVal = value.Get<Vector2>().x;
        _horizontalDir = inputVal;
    }
}
