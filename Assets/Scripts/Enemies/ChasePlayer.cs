using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    public bool isChasing => GetComponent<Enemy>().IsChasing;

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
        Vector2 newPos = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPos, GetComponent<EnemyPatrol>().GetSpeed() * Time.deltaTime);
    }

}
