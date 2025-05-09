using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector2 collisionSize;
    [SerializeField] private LayerMask playerLayer;

    void Start()
    {
    }

    void Update()
    {
        Collider2D[] colliders;

        colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), collisionSize, transform.rotation.z, playerLayer);

        if (colliders.Length == 0)
        {
            GetComponent<Collider2D>().enabled = true;
            return;
        }

        if (Input.GetKey("s") || Input.GetKey("space"))
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), collisionSize);
    }
}
