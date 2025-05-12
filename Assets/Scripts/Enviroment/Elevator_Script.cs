using UnityEngine;

public class Elevator_Script : MonoBehaviour
{
    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;

    private bool isPlayer;
    private bool isMoving;
    private bool buttonSwitch;
    private string level = "1";


    public float Speed = 5.0f;


    private Vector3 direction;
    private Vector3 targetPosition;

    private void Start()
    {
        isMoving = false;
        buttonSwitch= false;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction * Speed * Time.deltaTime);

            if (level == "1")
            {
                targetPosition = PatrolPointDown.transform.position;
                direction = (targetPosition - transform.position).normalized;
            }
            else if (level == "2")
            {
                targetPosition = PatrolPointUp.transform.position;
                direction = (targetPosition - transform.position).normalized;
            }

            if (Vector2.Distance(transform.position, targetPosition) < 0.00000001f)
            {
                isMoving = false;
                if (level == "1") level = "2";
                else if (level == "2") level = "1";
            }
        }
        Debug.Log(buttonSwitch);

        if(buttonSwitch)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
        }
    }

    public void OnElevator()
    {
        buttonSwitch = !buttonSwitch;
      
    }
}
