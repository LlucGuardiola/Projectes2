using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [SerializeField] private GameObject redCircle;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRate = 3f;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCooldown;

    private bool count;
    private float counter;
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
            animator.SetTrigger("MeleeAttack");
            Invoke("Attack", 0.4f);
            attackCooldown = attackRate;
        }
        Count();
    }

    private void Attack()
    {
        if (GetComponent<Enemy>().IsAttacking) return;

        GetComponent<Enemy>().IsAttacking = true;
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
            GetComponent<Enemy>().IsAttacking = false;
            count = false;
        }
    }
}
