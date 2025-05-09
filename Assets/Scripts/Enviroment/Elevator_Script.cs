using UnityEngine;

public class Elevator_Script : MonoBehaviour
{
    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;

    private bool isPlayer;
    private bool isMoving;

    public float Speed = 5.0f;


    private Vector3 direction;

    private void Start()
    {
        isPlayer = false;
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction * Speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, PatrolPointDown.transform.position) < 0.1f)
            {
                isMoving = false; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
            isMoving = true;

            direction = (PatrolPointDown.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
