using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 grabSize;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    protected bool CanGrab()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), grabSize, 0 /*angle*/, playerLayer);

        if (colliders.Length == 0) return false;

        foreach (var collision in colliders)
        {
            if (collision.gameObject == player) return true;
        }
        
        return false;
    }

    public virtual void Grab() { Destroy(gameObject); }
}
