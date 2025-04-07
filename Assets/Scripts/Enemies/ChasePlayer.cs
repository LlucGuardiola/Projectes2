using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    public bool isChasing => GetComponent<Enemy>().IsChasing;
    private 

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
        if (player.transform.position.x > transform.position.x && transform.localScale.x < 0 ||  player.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
