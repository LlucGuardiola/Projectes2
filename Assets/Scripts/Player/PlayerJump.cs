using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrengh;

    private bool canJump;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;

    private float coyoteTime = 0.2f;
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
    }

    public void OnJump()
    {
        if (!canJump && coyoteTimeCounter < 0) return;

        var vel = new Vector2(_rigidbody.linearVelocity.x, JumpStrengh);
        _rigidbody.linearVelocity = vel;
        canJump = false;

        //if (audioSource != null && JumpSound != null)
        //{
        //    audioSource.PlayOneShot(JumpSound);
        //}
    }
}