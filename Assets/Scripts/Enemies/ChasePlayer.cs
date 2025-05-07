using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    public bool isChasing => GetComponent<Enemy>().IsChasing;
    [SerializeField] private float maxDistanceToTarget;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), maxDistanceToTarget, GetComponent<Enemy>().WhatIsPlayer);
        
        if (colliders.Length != 0)
        {
            GetComponent<Enemy>().HasToChase = false;
            GetComponent<Enemy>().InRange = true;
        }
        else 
        {
            GetComponent<Enemy>().HasToChase = true;
            GetComponent<Enemy>().InRange = false;
        }

        if (GetComponent<Enemy>().HasToChase)
        {
            Vector2 newPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, newPos, GetComponent<Enemy>().ChaseSpeed * Time.deltaTime);
        }
        
        if (player.transform.position.x > transform.position.x && transform.localScale.x < 0 ||  player.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            GetComponent<Enemy>().Flip();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), maxDistanceToTarget);
    }
}
