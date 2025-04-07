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
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + visionRange, transform.position.y), visionSize);
    }
}
