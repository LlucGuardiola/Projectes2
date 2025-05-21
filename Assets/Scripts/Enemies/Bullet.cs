using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    // private Rigidbody2D _rigidbody;
    // private GameObject player;
    public Vector2 Direction;
    private float destroyTimeInstantiate = 1f;
    private float destroyTime;
    private Camera mainCamera;

    public bool BulletParried;

    void Start()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Direction.Normalize();
        destroyTime = destroyTimeInstantiate;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        BulletParried = false;
    }

    void Update()
    {
        mainCamera = Object.FindAnyObjectByType<Camera>();
        transform.position += (Vector3)Direction * speed * Time.deltaTime;
        destroyTime -= Time.deltaTime;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if object is outside the viewport
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 targetPosition)
    {
        Direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BulletParried && collision.gameObject.layer == 6)
        {
            GameObject player = GameObject.Find("Player");
            float multy = 1f;
            if (player != null) { multy = player.GetComponent<Parry>().MultiplyDamageAndReset(); }

            collision.GetComponent<Health>().TakeDamage(damage * multy);
            Destroy(gameObject);
            return;
        }
        
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        } 
        else if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
