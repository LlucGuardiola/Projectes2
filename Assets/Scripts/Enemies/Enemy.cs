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
          //  CheckForPlayer();
        }
    }


}
