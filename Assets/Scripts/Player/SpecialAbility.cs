using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpecialAbility : MonoBehaviour
{
    public static Action<Vector2> OnDash;
    [SerializeField] private LayerMask enemiesLayer;
    private bool count;
    private float counter;
    [SerializeField] private float dashCooldown;
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

        if (PauseLogic.IsPaused) return;
        if (!GetComponent<PlayerAttack>().CanAttack) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        Collider2D[] colliders = Physics2D.OverlapBoxAll(mouseWorldPosition, new Vector2(1, 1), 0, enemiesLayer);

        if (colliders.Length == 0) return;

        canDash = false;
        count = true;
        counter = 0;

        Debug.Log(colliders[0].name);

        if(Vector2.Distance(transform.position, colliders[0].gameObject.transform.position) < dashDistance)
        {
            OnDash?.Invoke(colliders[0].gameObject.transform.position);
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
