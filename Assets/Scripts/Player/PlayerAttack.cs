using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class PlayerAttack : MonoBehaviour
{
    public bool CanAttack;
    [HideInInspector] public bool isAttacking = false;
    [SerializeField] private LayerMask enemiesLayer;

    private bool count;
    private float counter;
    private float attackDuration;
    [SerializeField] private float basicAttackDuration;
    [SerializeField] private float dashAttackDuration;
    public float AttackRange;
    public Vector2 AttackSize;
    public GameObject RedCircle;

    private void Update()
    {
        Count();
        if (isAttacking) RedCircle.SetActive(true);      
        else RedCircle.SetActive(false);
    }
    private void OnEnable()
    {
        AttackSystem.OnAttackDone += Attack;
        Dash.OnDashEnd += Attack;
    }

    private void OnDisable()
    {
        AttackSystem.OnAttackDone -= Attack;
        Dash.OnDashEnd -= Attack;
    }

    private void Attack(float damageDealt, bool justDashed, Vector2 direction)
    {
        if (!CanAttack) return;
        if (isAttacking) return;

        GetComponent <PlayerMovement>().CanMove = false;

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

        float leftOrRight = GetComponent<PlayerMovement>().LookingForward ? AttackRange : -AttackRange;

        Collider2D[] colliders;

        if (!justDashed) 
        {
            colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + leftOrRight , transform.position.y), AttackSize, transform.rotation.z, enemiesLayer);
            attackDuration = basicAttackDuration;
        }
        else
        {
            colliders = Physics2D.OverlapBoxAll((Vector2)transform.position + direction * AttackRange, AttackSize, transform.rotation.z, enemiesLayer);
            attackDuration = dashAttackDuration;
        }

        if (colliders.Length == 0) return;

        foreach (var enemy in colliders)
        {
            enemy.gameObject.GetComponent<Health>().TakeDamage(damageDealt);
        }
    }

    private void Count()
    {
        if (!count) return;
        
        counter += Time.deltaTime;

        if (counter >= attackDuration)
        {
            isAttacking = false;
            transform.rotation = Quaternion.identity;
            GetComponent<PlayerMovement>().CanMove = true;
            count = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + AttackRange, transform.position.y), AttackSize);
    }
}
