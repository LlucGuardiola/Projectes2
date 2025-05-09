using UnityEngine;

public class Elevator_Script : MonoBehaviour
{
    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;

    private bool isPlayer;
    private bool isMoving;
    private bool buttonSwitch;

    public float Speed = 5.0f;


    private Vector3 direction;

    private void Start()
    {
        isMoving = false;
        buttonSwitch= false;
    }

    private void Update()
    {
        if (isMoving && buttonSwitch)
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
            isMoving = true;

            direction = (PatrolPointDown.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMoving = false;
        }
    }

    public void OnElevator()
    {
        buttonSwitch = true;
    }
}
