using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class OnAttack : MonoBehaviour
{
    [HideInInspector] public bool isAttacking = false;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private float attackRange;
    public GameObject redCircle;
    private bool count;
    private float counter;

    private void Update()
    {
        Count();
        if (isAttacking) redCircle.SetActive(true);      
        else redCircle.SetActive(false);
    }
    private void OnEnable()
    {
        AttackSystem.OnAttackDone += Attack;
    }

    private void OnDisable()
    {
        AttackSystem.OnAttackDone -= Attack;
    }

    private void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        count = true;
        counter = 0;

        int animationNum = UnityEngine.Random.Range(0,2);

        switch (animationNum)
        {
            case 0:        //anim 0
                break;
            case 1:        //anim 1      
                break;
            default:       //anim 2
                break;      
        }

        float position = GetComponent<PlayerMovement>().LookingForward ? attackRange : -attackRange;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + position, transform.position.y), attackSize, 0, enemiesLayer);

        if (colliders.Length == 0) return;

        foreach (var enemy in colliders)
        {
            enemy.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= 0.2f)
        {
            isAttacking = false;
            count = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + attackRange, transform.position.y), attackSize);
    }
}
