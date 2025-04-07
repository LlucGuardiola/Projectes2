using UnityEngine;

public class Enemy : MonoBehaviour
{
 // [SerializeField] private float detectionRange = 5f;
 // [SerializeField] private float visionAngle = 90f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;
    private VisionDetection vision;
    public bool IsChasing { get; set; }
    public bool IsPatrolling => !IsChasing;
    public float loseSightCooldown = 2f;
    private float loseSightTimer = 0f;

    private void Start()
    {
    }
}
