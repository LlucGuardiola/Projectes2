using System.Collections.Generic;
using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    [SerializeField] private float visionRangeX;
    [SerializeField] private float visionRangeY;

    [SerializeField] private Vector2 visionSize;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        if (!PlayerInventory.PlayerHasSword) return;
        if (GetComponent<Enemy>().IsDead) return;

        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRangeX : -visionRangeX;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight, transform.position.y + visionRangeY / 2 - 0.5f), visionSize, transform.rotation.z, playerLayer);
        
        if (colliders.Length == 0) 
        {
            if (GetComponent<Enemy>().IsChasing)
            {
                GetComponent<Enemy>().IsPatrolling = true; 
                GetComponent<Enemy>().IsChasing = false;

                Vector2 currentTarget = GetComponent<EnemyPatrol>().currentTarget.transform.position;

                if (currentTarget.x > transform.position.x && !GetComponent<Enemy>().LookingForward ||
                    currentTarget.x < transform.position.x && GetComponent<Enemy>().LookingForward) 
                {
                    GetComponent<Enemy>().Flip();
                }
            }
            return;
        }

        if (GetComponent<Enemy>().IsPatrolling)
        {
            GetComponent<Enemy>().IsChasing = true;
            GetComponent<Enemy>().IsPatrolling = false;
        }
    }

    private void OnDrawGizmos()
    {
        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRangeX : -visionRangeX;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + leftOrRight, transform.position.y + visionRangeY), visionSize);
    }
}
