using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float damage = 1.0f;
   // private Rigidbody2D _rigidbody;
    //private GameObject player;
    private Vector2 direction;
    

    void Start()
    {
   //     _rigidbody = GetComponent<Rigidbody2D>();
      //  _rigidbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
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
        }
    }
}
