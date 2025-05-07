using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private GameObject redCircle;
    private Animator animator;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("MeleeAttack");
        isAttacking = true;
        count = true;
        counter = 0;
        redCircle.transform.localScale = new Vector2(redCircle.transform.localScale.x + 3.5f, redCircle.transform.localScale.y + 3.5f);
        
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(redCircle.transform.position.x, redCircle. transform.position.y), new Vector2 (2,2), transform.rotation.z, GetComponent<Enemy>().WhatIsPlayer);
        if (colliders.Length == 0) return;
        colliders[0].gameObject.GetComponent<Health>().TakeDamage(1f);

    }
    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= attackDuration)
        {
            redCircle.transform.localScale = new Vector2(redCircle.transform.localScale.x - 3.5f, redCircle.transform.localScale.y - 3.5f);
            isAttacking = false;
            count = false;
        }
    }
}
