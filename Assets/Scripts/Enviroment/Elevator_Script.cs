using UnityEngine;

public class Elevator_Script : MonoBehaviour
{
    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;
    public GameObject Elevator;

    private bool isMoving;
    private bool buttonSwitch;
    private string level = "1";
    public bool key;
    private bool isPlayerInside;

    public float Speed = 5.0f;
    private Vector3 direction;
    private Vector3 targetPosition;

    private void Start()
    {
        isMoving = false;
        buttonSwitch= false;
        isPlayerInside = false;
    }

    private void Update()
    {
        if (buttonSwitch && !isMoving)
        {
            isMoving = true;

            if (level == "1")
            {
                targetPosition = PatrolPointDown.transform.position;
                direction = (targetPosition - Elevator.transform.position).normalized;
            }
            else if (level == "2")
            {
                targetPosition = PatrolPointUp.transform.position;
                direction = (targetPosition - Elevator.transform.position).normalized;
            }
        }

        if (isMoving)
        {
            Elevator.transform.Translate(direction * Speed * Time.deltaTime);

            if (Vector3.Distance(Elevator.transform.position, targetPosition) < 0.1f)
            {
                Elevator.transform.position = targetPosition;
                isMoving = false;
                buttonSwitch = false;
                if (level == "1") level = "2"; else level = "1";
            }
        }
    }

    public void OnElevator()
    {
        if(key && isPlayerInside)
        {
            buttonSwitch = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player");
            isPlayerInside = true;  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("playerlolo");
            isPlayerInside = false;
        }
    }
}
