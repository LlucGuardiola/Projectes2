using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRate = 1.5f;
    private float attackCooldown;
    [HideInInspector] public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 1f && GetComponent<Enemy>().InRange && GetComponent<Enemy>().MeleeAttack)
        {
            Attack(player.transform);
            attackCooldown = attackRate;
        }
    }

    private void Attack(Transform player)
    {
        Debug.Log("atac");
    }
}
