using UnityEngine;

public class Enemy : MonoBehaviour
{





/*
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
                    isChasing = true;
                    break;
                }
            }
        }
    }*/
}
