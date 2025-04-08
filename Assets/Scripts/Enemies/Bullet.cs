using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
   // private Rigidbody2D _rigidbody;
    //private GameObject player;
    private Vector2 direction;
    private float destroyTimeInstantiate = 1f;
    private float destroyTime;
    private Camera mainCamera;

    void Start()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        destroyTime = destroyTimeInstantiate;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        //     _rigidbody = GetComponent<Rigidbody2D>();
        //  _rigidbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = Object.FindAnyObjectByType<Camera>();
        transform.position += (Vector3)direction * speed * Time.deltaTime;
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
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Entra");
            }
            Destroy(gameObject);
        } else if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
