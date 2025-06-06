using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SmallHedge.SoundManager;


public class SpecialAbility : MonoBehaviour
{
    public static Action<GameObject> OnDash;
    [SerializeField] private LayerMask enemiesLayer;
    private bool count;
    private float counter;
    public float dashCooldown;
    private bool canDash;
    [SerializeField] private float dashDistance;

    private void Start()
    {
        canDash = true;
    }
    private void Update()
    {
        Count();
    }

    public void OnSpecialAbility()
    {
        if (!canDash) return;
        if (CheckpointManager.IsDead) return;

        if (PauseLogic.IsPaused) return;
        if (!GetComponent<PlayerAttack>().CanAttack) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        Collider2D[] colliders = Physics2D.OverlapBoxAll(mouseWorldPosition, new Vector2(1, 1), 0, enemiesLayer);

        if (colliders.Length == 0) return;

        canDash = false;
        count = true;
        counter = 0;

        if(Vector2.Distance(transform.position, colliders[0].gameObject.transform.position) < dashDistance)
        {
            if (!colliders[0].gameObject.GetComponent<Enemy>().IsDead)
            {
                OnDash?.Invoke(colliders[0].gameObject);
            }
        }
    }

    private void Count()
    {
        if (!count) return;

        counter += Time.deltaTime;

        if (counter >= dashCooldown)
        {
            canDash = true;
            count = false;
        }
    }
}
