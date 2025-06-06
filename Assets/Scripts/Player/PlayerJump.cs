using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using SmallHedge.SoundManager;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrengh;
    [SerializeField] private float WallJumpStrength;
    [HideInInspector] public bool IsWallJumping;

    private bool canJump;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    private Animator animator;

    int CollisionPos => _collisionDetection.CollisionPos;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private bool count;
    private float counter;

    public AudioClip jumpStart;
    public AudioClip jumpEnd;

    private AudioSource audioSource;

    [HideInInspector] public bool IsWallSliding => _collisionDetection.IsTouchingFront;
    [HideInInspector] public bool IsTouchingGround => _collisionDetection.IsGrounded;

    void Start()
    {
        counter = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
        IsWallJumping = false;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyUp("space")) coyoteTimeCounter = 0;

        animator.SetBool("OnAir", !IsTouchingGround);

        if (IsTouchingGround)
        {
            coyoteTimeCounter = coyoteTime;
            _rigidbody.gravityScale = 7;
        }
        else coyoteTimeCounter -= Time.deltaTime;
        
        if (!IsTouchingGround && canJump)
        {
            canJump = false;
        }

        if (IsWallSliding && !IsTouchingGround)
        {
            canJump = true;
            animator.SetBool("WallSliding", true);
        }
        else
        {
            animator.SetBool("WallSliding", false);
        }

        Count();
    }

    public void OnJump()
    {
        if (PauseLogic.IsPaused) return;
        if (!canJump && coyoteTimeCounter < 0) return;
        if (CheckpointManager.IsDead) return;

        SoundManager.PlaySound(SoundType.Attack);


        var vel = new Vector2(_rigidbody.linearVelocity.x * 1.5f, JumpStrengh);

        if (IsWallSliding && !IsTouchingGround)
        {
            vel = new Vector2(-CollisionPos * WallJumpStrength, JumpStrengh); 
            count = true;
            counter = 0;
            IsWallJumping = true;

            if (GetComponent<PlayerMovement>().LookingForward == false && CollisionPos < 0)
            {
                GetComponent<PlayerMovement>().LookingForward = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (GetComponent<PlayerMovement>().LookingForward && CollisionPos > 0)
            {
                GetComponent<PlayerMovement>().LookingForward = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }   

        GetComponent<PlayerMovement>().BlockHorizontalMovement(0.2f);

        _rigidbody.linearVelocity = vel;

        Invoke("SlowGravity", 0.4f);
        canJump = false;
    }

    private void Count()
    {
        if (!count) return;

        animator.SetBool("IsDashing", true);
        counter += Time.deltaTime;
        transform.rotation = GetComponent<PlayerMovement>().LookingForward ? Quaternion.Euler(0, 0, 45) : Quaternion.Euler(0, 0, -45);

        if (counter >= 0.2f)
        {
            animator.SetBool("IsDashing", false);
            transform.rotation = Quaternion.identity;
        }

        if (counter >= 0.4f)
        {
            IsWallJumping = false;
            count = false;
        }
    }

    private void SlowGravity()
    {
        _rigidbody.gravityScale = 4f;
    }
}
