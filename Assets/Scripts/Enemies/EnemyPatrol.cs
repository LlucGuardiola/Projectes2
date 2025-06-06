
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

    void Update()
    {
        if (GetComponent<Enemy>().IsDead) return;
        if (GetComponent<Enemy>().IsAttacking) return;

        int x = transform.position.x < currentTarget.position.x ? -1 : 1;

        GetComponent<Enemy>().direction = new Vector2(x, 0);

        if (GetComponent<Enemy>().PatrollingDisabled)
        {
            GetComponent<Animator>().SetBool("IsIdle?", true);
            GetComponent<Enemy>().direction = new Vector2(-x, 0);
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
        
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }
    }
}
