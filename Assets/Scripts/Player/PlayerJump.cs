using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrengh;

    private bool canJump;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    int CollisionPos => _collisionDetection.CollisionPos;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private bool count;
    private float counter;

    [HideInInspector] public bool IsWallSliding => _collisionDetection.IsTouchingFront;

    [HideInInspector] public bool IsTouchingGround => _collisionDetection.IsGrounded;

    void Start()
    {
        counter = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
    }

    private void Update()
    {
        if (Input.GetKeyUp("space")) coyoteTimeCounter = 0; 

        if (IsTouchingGround) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;


        GetComponent<Animator>().SetBool("IsOnAir?", !IsTouchingGround);

        if (!IsTouchingGround && canJump)
        {
            canJump = false;
        }

        if (IsWallSliding)
        {
            canJump = true;
        }

        Count();
    }

    public void OnJump()
    {
        if (!canJump && coyoteTimeCounter < 0) return;


        var vel = new Vector2(_rigidbody.linearVelocity.x, JumpStrengh);

        if (IsWallSliding && !IsTouchingGround) 
        {
            GetComponent<PlayerMovement>().CanMove = false;
            vel = new Vector2(-CollisionPos * JumpStrengh / 3, JumpStrengh);
            count = true;
            counter = 0;

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

        _rigidbody.linearVelocity = vel;
        canJump = false;

        //if (audioSource != null && JumpSound != null)
        //{
        //    audioSource.PlayOneShot(JumpSound);
        //}
    }

    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= 0.4f)
        {
            GetComponent<PlayerMovement>().CanMove = true;
            count = false;
        }
    }
}