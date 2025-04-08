using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;

    public Transform currentTarget;
    public bool isPatrolling => GetComponent<Enemy>().IsPatrolling;

    void Start()
    {
        currentTarget = patrolPointB.transform;
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
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, GetComponent<Enemy>().PatrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            GetComponent<Enemy>().Flip();

            currentTarget = (currentTarget == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }
    }
}
