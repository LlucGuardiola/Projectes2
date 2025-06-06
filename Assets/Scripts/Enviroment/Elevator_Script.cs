
using UnityEngine;
using UnityEngine.Tilemaps;



public class Elevator_Script : MonoBehaviour
{
    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;
    public GameObject Elevator;
    public GameObject ElevatorDoor,ElevatorDoorSecond;
    public GameObject Door1,Door2, floor;

    private bool isMoving;
    private bool buttonSwitch;
    private bool isPlayerInside;
    private bool levelTwo;

    public float Speed = 5.0f;
    private Vector3 direction;
    private Vector3 targetPosition;
    private Animation firstAnimation,secondAnimation;
    private BoxCollider2D[] boxColliders;

    private void Start()
    {
        boxColliders = Elevator.GetComponents<BoxCollider2D>();
        isMoving = false;
        buttonSwitch= false;
        isPlayerInside = false;
        levelTwo = false;
        firstAnimation = ElevatorDoor.GetComponent<Animation>();
        secondAnimation = ElevatorDoorSecond.GetComponent<Animation>();
    }

    private void Update()
    {
        if (buttonSwitch && !isMoving)
        {
            isMoving = true;
            targetPosition = PatrolPointUp.transform.position;
            direction = (targetPosition - Elevator.transform.position).normalized;
            
        }

        if (isMoving)
        {
            Elevator.transform.Translate(direction * Speed * Time.deltaTime);

            if (Vector3.Distance(Elevator.transform.position, targetPosition) < 0.1f)
            {
                Elevator.transform.position = targetPosition;
                isMoving = false;
                buttonSwitch = false;
                secondAnimation.Play("Opendoor");
                ElevatorDoorSecond.GetComponent<SpriteRenderer>().sortingOrder = 5;
               
                Invoke("CloseSecondDoor", 3.0f);
                Door1.GetComponent<SpriteRenderer>().sortingOrder = 5;
                Door2.GetComponent<SpriteRenderer>().sortingOrder = 5;
                floor.GetComponent<TilemapRenderer>().sortingOrder = 5;
                floor.GetComponent<BoxCollider2D>().enabled = true;
                foreach (var collider in boxColliders)
                {
                    collider.enabled = false;
                }
                buttonSwitch = false;
                levelTwo = true;
            }
        }
    }

    public void OnElevator()
    {
       
        if(isPlayerInside && !levelTwo)
        {
            foreach( var  collider in boxColliders )
            {
                collider.enabled = true;
            }
            firstAnimation.Play("Opendoor");//Opendoor
            ElevatorDoor.GetComponent<SpriteRenderer>().sortingOrder = 7;
            Invoke("SwitchButton", 1.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            isPlayerInside = true;  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            isPlayerInside = false;
        }
    }

    private void SwitchButton()
    {
        buttonSwitch = true;
        Invoke("CloseDoor", 0.5f);
    }

    private void CloseDoor()
    {
        firstAnimation.Play("CloseDore");
    }

    private void CloseSecondDoor()
    {
        secondAnimation.Play("CloseDore");
    }

}
