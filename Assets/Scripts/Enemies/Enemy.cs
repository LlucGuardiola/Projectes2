using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;

    public bool IsChasing = false;
    public bool IsPatrolling = false;


    private void Update()
    {
        if (IsPatrolling)
        {
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, whatIsPlayer);
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
                    IsChasing = true;
                    IsPatrolling = false;
                    Debug.Log("h");
                    break;
                }
            }
        }
    }
}
