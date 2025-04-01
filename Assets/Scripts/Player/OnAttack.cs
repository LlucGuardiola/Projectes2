using System;
using UnityEngine;


public class OnAttack : MonoBehaviour
{
    [HideInInspector] public bool isAttacking = false;
    [SerializeField] private LayerMask enemiesLayer;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, enemiesLayer);

        if (colliders.Length == 0) return;

        foreach (var enemy in colliders)
        {
            enemy.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }
}
