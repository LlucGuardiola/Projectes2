using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;
    private GameObject player;
    public bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isChasing)
        {
            Chase();
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, GetComponent<EnemyPatrol>().GetSpeed() * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
        {
            isChasing = true;
        }
    }

}
