
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;

    [HideInInspector] public Transform currentTarget;
    public bool isPatrolling => GetComponent<Enemy>().IsPatrolling;

    void Start()
    {
        currentTarget = patrolPointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy>().IsDead) return;
        if (GetComponent<Enemy>().IsAttacking) return;

        if (GetComponent<Enemy>().PatrollingDisabled && !GetComponent<Enemy>().IsChasing)
        {
            GetComponent<Animator>().SetBool("IsIdle?", true);   
            return;
        }

        if (isPatrolling)
        {
            Patrol();
        }
    }
    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, GetComponent<Enemy>().PatrolSpeed * Time.deltaTime);
        
        int x = transform.position.x < currentTarget.position.x ? -1 : 1;

        GetComponent<Enemy>().direction = new Vector2(x, 0);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }
    }
}
