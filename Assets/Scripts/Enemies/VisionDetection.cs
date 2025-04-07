using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    [SerializeField] private float visionRange;
    [SerializeField] private Vector2 visionSize;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRange : -visionRange;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight, transform.position.y), visionSize, transform.rotation.z, playerLayer);
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

    private void OnDrawGizmos()
    {
        float leftOrRight = GetComponent<Enemy>().LookingForward ? visionRange : -visionRange;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + leftOrRight, transform.position.y), visionSize);
    }
}
