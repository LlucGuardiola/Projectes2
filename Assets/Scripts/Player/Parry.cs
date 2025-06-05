using UnityEngine;

public class Parry : MonoBehaviour
{
    public float ParryRange;
    public Vector2 ParrySize;
    public bool CanParry;
    public bool IsParring;
    private bool count;
    private float counter;
    private bool instantReset;
    private Animator animator;
    private PlayerJump playerJump;
    PlayerMovement playerMovement;

    [SerializeField] private float parryCooldown;

    [SerializeField] private float parryDuration;

    [SerializeField] private float parryDamageMultiplier;

    [SerializeField] private LayerMask bulletLayer;

    private void Start()
    {
        CanParry = true;
        counter = 0;
        instantReset = false;
        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (!PlayerInventory.PlayerHasSword) return;
        if (CheckpointManager.IsDead) return;   

        if (Input.GetMouseButtonDown(1))
        {
            if (CanParry && !IsParring && playerJump.IsTouchingGround)
            {
                playerMovement.CanMove = false;
                IsParring = true;
                count = true;
                counter = 0;
                ParryAnimationController.TriggerParryAnimation();
            }
        }

        animator.SetBool("IsParring", IsParring);
        if (IsParring) Parr();

        Count();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + ParryRange, transform.position.y), ParrySize);
    }
    private void Parr()
    {
        float leftOrRight = GetComponent<PlayerMovement>().LookingForward ? ParryRange : -ParryRange;

        Collider2D[] colliders;
        colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight, transform.position.y), ParrySize, transform.rotation.z, bulletLayer);

        if (colliders.Length == 0) return;

        foreach (var bullet in colliders)
        {
            if (!bullet.gameObject.GetComponent<Bullet>().BulletParried)
            {
                Vector2 randomDirection = new Vector2(leftOrRight, Random.Range(-0.9f, 0.9f)).normalized;

                bullet.gameObject.GetComponent<Bullet>().Direction = randomDirection;
                bullet.gameObject.GetComponent<Bullet>().BulletParried = true;
            }
        }

        instantReset = true;
    }
    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= parryDuration)
        {
            IsParring = false;
            count = false;
            ParryAnimationController.EndParryAnimation();
            CanParry = false;
            animator.SetBool("IsParring", false);
            playerMovement.CanMove = true;

            if (instantReset) 
            {
                Invoke("EnableParry", 0);
                instantReset = false;
            } 
            else
            {
                Invoke("EnableParry", parryCooldown);
            }
        }
    }
    private void EnableParry()
    {
        CanParry = true;
    }

    public float MultiplyDamageAndReset()
    {
        return parryDamageMultiplier;
    }
}
