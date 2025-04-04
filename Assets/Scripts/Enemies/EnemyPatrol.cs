using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;
    [SerializeField] private float patrolSpeed = 2f;
    private Transform currentPoint;
    public bool isPatrolling => GetComponent<Enemy>().IsPatrolling;

    void Start()
    {
        currentPoint = patrolPointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
    
    }
    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            Debug.Log("i");
            Flip();
            currentPoint = (currentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;

        }
    }

    private void Flip()
    {
        Debug.Log("h");
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        GetComponent<VisionDetection>().AngleDirection *= -1;
        Debug.Log("g");
    }

    public float GetSpeed()
    {
        return patrolSpeed;
    }
}
