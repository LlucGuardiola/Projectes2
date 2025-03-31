using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAbility : MonoBehaviour
{
    public static Action<Vector2> OnDash;
    [SerializeField] private LayerMask enemiesLayer;

    public void OnSpecialAbility()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        var colliders = Physics2D.OverlapBoxAll(mouseWorldPosition, new Vector2(1, 1), 0, enemiesLayer);

        if (colliders.Length == 0) return;

        Debug.Log(colliders[0].name);

        GetComponent<PlayerMovement>().CanMove = false;
        OnDash?.Invoke(colliders[0].transform.position);
    }
}
