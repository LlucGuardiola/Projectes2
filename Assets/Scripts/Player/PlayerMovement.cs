using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool CanMove;

    [SerializeField] private float Speed = 5.0f;

    private float inputVal;
    private float _horizontalDir;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    [HideInInspector] public bool LookingForward;
    private float initialGravity;

    int CollisionPos => _collisionDetection.CollisionPos;

    void Start()
    {
        LookingForward = true;
        CanMove = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
        initialGravity = _rigidbody.gravityScale;
    }

    void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("IsRunning?", _rigidbody.linearVelocity.x != 0 && GetComponent<PlayerJump>().IsTouchingGround);
       // Debug.Log(CanMove);

        if (GetComponent<Dash>().IsDashing) return;

        if (!CanMove) 
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector3.zero;
            return;
        }
        else if (_rigidbody.gravityScale == 0) { _rigidbody.gravityScale = initialGravity; }


        _horizontalDir = inputVal;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;

        if ((CollisionPos == 1 && _horizontalDir > 0) || (CollisionPos == -1 && _horizontalDir < 0))
        {
            velocity.x = 0;
        }

        _rigidbody.linearVelocity = velocity;
    }

    void OnMove(InputValue value)
    {
        if (PauseLogic.IsPaused) return;
        if (!CanMove) return;

        inputVal = value.Get<Vector2>().x;
        _horizontalDir = inputVal;

        if (inputVal > 0 && !LookingForward) // right
        {
            LookingForward = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (inputVal < 0 && LookingForward) // left
        {
            LookingForward = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
