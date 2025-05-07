using UnityEngine;

public class Parry : MonoBehaviour
{
    public float ParryRange;
    public Vector2 ParrySize;
    [SerializeField] private LayerMask bulletLayer;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey("t"))
        {
            Debug.Log("parryStart");

            float leftOrRight = GetComponent<PlayerMovement>().LookingForward ? ParryRange : -ParryRange;

            Collider2D[] colliders;
            colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight, transform.position.y), ParrySize, transform.rotation.z, bulletLayer);

            if (colliders.Length == 0) return;
            Debug.Log("parry");

            foreach (var bullet in colliders)
            {
                Debug.Log("parry");
                bullet.gameObject.GetComponent<Bullet>().Direction *= -1;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + ParryRange, transform.position.y), ParrySize);
    }
}
