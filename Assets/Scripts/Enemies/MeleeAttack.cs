using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRate = 3f;
    private float attackCooldown;
    [HideInInspector] public GameObject player;
    private bool count;
    private float counter;
    private bool isAttacking;
    [SerializeField] private float attackDuration;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f && GetComponent<Enemy>().InRange && GetComponent<Enemy>().MeleeAttack)
        {
            Attack(player.transform);
            attackCooldown = attackRate;
        }
        Count();
    }

    private void Attack(Transform player)
    {
        if (isAttacking) return; 
        isAttacking = true;
        count = true;
        counter = 0;
    }
    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= attackDuration)
        {
            isAttacking = false;
            count = false;
        }
    }
}
