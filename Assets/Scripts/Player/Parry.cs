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

    [SerializeField] private float parryCooldown;

    [SerializeField] private float parryDuration;

    [SerializeField] private float parryDamageMultiplier;

    [SerializeField] private LayerMask bulletLayer;

    private void Start()
    {
        CanParry = true;
        counter = 0;
        instantReset = false;
    }

    void Update()
    {
        if (!PlayerInventory.PlayerHasSword) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (CanParry && !IsParring)
            {
                IsParring = true;
                count = true;
                counter = 0;
                ParryAnimationController.TriggerParryAnimation();
            }
        }

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
            bullet.gameObject.GetComponent<Bullet>().Direction *= -1;
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
