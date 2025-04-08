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
        transform.position = Vector2.MoveTowards(transform.position, newPos, GetComponent<Enemy>().ChaseSpeed * Time.deltaTime);
        
        if (player.transform.position.x > transform.position.x && transform.localScale.x < 0 ||  player.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            GetComponent<Enemy>().Flip();
        }
    }
}
