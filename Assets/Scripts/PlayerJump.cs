using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrengh;

    private bool canJump;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;

    private float coyoteTIme = 0.2f;
    private float coyoteTimeCounter;

    bool IsWallSliding => _collisionDetection.IsTouchingFront;
    [HideInInspector] public bool IsTouchingGround => _collisionDetection.IsGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
    }

    void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("IsOnAir?", !IsTouchingGround);

        if (IsTouchingGround && _rigidbody.linearVelocity.y < 0.1f) // Method used to avoid checking if is IsTouchingGround when the player just jumped
        {
            canJump = true;
        }

        if (IsTouchingGround) coyoteTimeCounter = coyoteTIme;
        else coyoteTimeCounter -= Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (!IsTouchingGround && canJump)
        {
            canJump = false;
        }
    }

    public void OnJump()
    {
        if (!canJump && !(coyoteTimeCounter > 0)) return;

        var vel = new Vector2(_rigidbody.linearVelocity.x, JumpStrengh);
        _rigidbody.linearVelocity = vel;
        canJump = false;

        coyoteTimeCounter = 0f;


        //if (audioSource != null && JumpSound != null)
        //{
        //    audioSource.PlayOneShot(JumpSound);
        //}
    }
}



