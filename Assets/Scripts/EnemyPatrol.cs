using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;
    [SerializeField] private float patrolSpeed = 2f;

    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;

    private Rigidbody2D rb;
    private Transform currentPoint;
    private GameObject player;
    private bool isChasing = false; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = patrolPointB.transform;
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        if (isChasing)
        {

        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, detectionRange);
        if (Vector2.Distance(transform.position, currentPoint.position)<0.1f)
        {
            currentPoint = (currentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }

    }

    private void CheckForPlayer()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position,detectionRange,whatIsPlayer); 
        foreach (var playerCollider in playersInRange)
        {
            Transform playerTransform = playerCollider.transform;
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);
            if (angleToPlayer < visionAngle / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, whatIsObstacle);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    isChasing = true;
                    break;
                }
            }
        }
    }



}
