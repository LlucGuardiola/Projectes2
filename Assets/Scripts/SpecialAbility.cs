using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAbility : MonoBehaviour
{
    public static Action<Vector2> OnDash;

    public void OnSpecialAbility()
    {
        GetComponent<PlayerMovement>().CanMove = false;
        OnDash?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
