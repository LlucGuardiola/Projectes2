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
    private Animator animator;
    private Dash dash;
    private PlayerJump playerJump;
    public ParticleSystem dust;

    int CollisionPos => _collisionDetection.CollisionPos;

    private bool blockHorizontalMovement = false;
    private float blockTimer = 0f;

    void Start()
    {
        LookingForward = true;
        CanMove = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
        initialGravity = _rigidbody.gravityScale;

        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
        dash = GetComponent<Dash>();
    }

    void FixedUpdate()
    {
        animator.SetBool("IsRunning?", (_rigidbody.linearVelocity.x >= 1.2f || _rigidbody.linearVelocity.x <= -1.2f) && GetComponent<PlayerJump>().IsTouchingGround);

        if (dash.IsDashing) return;
        if (playerJump.IsWallJumping) return;
        if (PauseLogic.IsPaused) return;

        if (!CanMove)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector3.zero;
            return;
        }
        else if (_rigidbody.gravityScale == 0)
        {
            _rigidbody.gravityScale = initialGravity;
        }

        if (blockHorizontalMovement)
        {
            blockTimer -= Time.fixedDeltaTime;
            if (blockTimer <= 0f)
            {
                blockHorizontalMovement = false;
            }
            return;
        }

        inputVal = Input.GetAxis("Horizontal");
        _horizontalDir = inputVal;



        if (inputVal > 0 && !LookingForward)
        {
            LookingForward = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (inputVal < 0 && LookingForward)
        {
            LookingForward = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;

        if ((CollisionPos == 1 && _horizontalDir > 0) || (CollisionPos == -1 && _horizontalDir < 0))
        {
            velocity.x = 0;
        }

        _rigidbody.linearVelocity = velocity;

        if (Mathf.Abs(inputVal) > 0.1f && playerJump.IsTouchingGround)
        {
            if (!dust.isPlaying)
                dust.Play();
        }
        else
        {
            if (dust.isPlaying)
                dust.Stop();
        }
    }

    public void BlockHorizontalMovement(float duration)
    {
        blockHorizontalMovement = true;
        blockTimer = duration;
    }

    
}
