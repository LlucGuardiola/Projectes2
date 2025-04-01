using System;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static Action OnAttackDone;
    public void OnAtack()
    {
        OnAttackDone?.Invoke();
    }
}
