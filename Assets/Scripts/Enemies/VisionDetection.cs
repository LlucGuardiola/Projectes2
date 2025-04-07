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
        if (colliders.Length == 0) return;

        foreach (var enemy in colliders)
        {
            Debug.Log("player detected");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + visionRange, transform.position.y), visionSize);
    }
}
