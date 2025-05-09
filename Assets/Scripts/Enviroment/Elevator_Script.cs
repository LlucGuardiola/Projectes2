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
                Debug.Log("Elevador llegó al punto inferior");
                isMoving = false; // Se detiene
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador activó el elevador");
            isPlayer = true;
            isMoving = true;

            direction = (PatrolPointDown.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del trigger");
            isPlayer = false;
        }
    }
}
