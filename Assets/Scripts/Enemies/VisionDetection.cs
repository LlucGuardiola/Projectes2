using System.Collections.Generic;
using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    [SerializeField] private float visionRangeX;
    [SerializeField] private float visionRangeY;

    [SerializeField] private Vector2 visionSize;
    [SerializeField] private LayerMask playerLayer; 
    [SerializeField] private LayerMask ObstacleLayer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!PlayerInventory.PlayerHasSword) return;
        if (GetComponent<Enemy>().IsDead) return;

        if (CheckLineOfSight() == 1) return;

        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRangeX : -visionRangeX;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight, transform.position.y + visionRangeY / 2 - 0.5f), visionSize, transform.rotation.z, playerLayer);
        
        if (colliders.Length == 0) 
        {
            if (GetComponent<Enemy>().IsChasing)
            {
                GetComponent<Enemy>().IsPatrolling = true; 
                GetComponent<Enemy>().IsChasing = false;
            }
            return;
        }

        if (GetComponent<Enemy>().IsPatrolling)
        {
            GetComponent<Enemy>().IsChasing = true;
            GetComponent<Enemy>().IsPatrolling = false;
        }
    }

    public int CheckLineOfSight()
    {
        Vector2 origin = transform.position;
        Vector2 destination = player.transform.position;
        Vector2 direction = (destination - origin).normalized;
        float distance = Vector2.Distance(origin, destination);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, ObstacleLayer);

        if (hit.collider != null)
        {
            return 1;
        }

        return 0;
    }

    private void OnDrawGizmos()
    {
        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRangeX : -visionRangeX;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + leftOrRight, transform.position.y + visionRangeY), visionSize);
    }
}
