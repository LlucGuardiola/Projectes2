using UnityEngine;

public class Elevator_Script : MonoBehaviour
{


    public GameObject PatrolPointUp;
    public GameObject PatrolPointDown;

    private bool isPlayer;

    public float Speed = 5.0f;

    [SerializeField] private GameObject triggerZone;
   

    private Vector3 direction;  
    private void Start()
    {
        isPlayer = false;
  
        if (isPlayer)
        {
            transform.position = PatrolPointUp.transform.position;
            direction = (PatrolPointUp.transform.position - PatrolPointDown.transform.position).normalized;
        } 
    }

    private void Update()
    {
       if(isPlayer) 
       {
            transform.Translate(direction * Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, PatrolPointUp.transform.position) < 0.1f)
            {
                direction = (PatrolPointDown.transform.position - PatrolPointUp.transform.position).normalized;
            }
            else if (Vector2.Distance(transform.position, PatrolPointDown.transform.position) < 0.1f)
            {
                direction = (PatrolPointUp.transform.position - PatrolPointDown.transform.position).normalized;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("1");
            isPlayer = true;
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {
            Debug.Log("2");
            isPlayer = false;
        }
    }

}
