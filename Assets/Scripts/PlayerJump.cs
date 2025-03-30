using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrengh;

    private bool canJump;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;

    bool IsWallSliding => _collisionDetection.IsTouchingFront;
    bool IsTouchingGround => _collisionDetection.IsGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
    }

    void FixedUpdate()
    {
        if (IsTouchingGround && _rigidbody.linearVelocity.y < 0.1f) // Method used to avoid checking if is IsTouchingGround when the player just jumped
        {
            canJump = true;
        }
    }

    private void Update()
    {
        // GetComponent<Animator>().SetBool("isOnAir?", !IsTouchingGround);
    }

    public void OnJump()
    {
        if (!canJump ) return;

        // GetComponent<Animator>().SetTrigger("startJump");

        var vel = new Vector2(_rigidbody.linearVelocity.x, JumpStrengh);
        _rigidbody.linearVelocity = vel;
        canJump = false;


        //if (audioSource != null && JumpSound != null)
        //{
        //    audioSource.PlayOneShot(JumpSound);
        //}
    }
}



