using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static Action<float, bool, Vector2> OnAttackDone;

    public void OnAttack()
    {
        if (PauseLogic.IsPaused) return;
        
        OnAttackDone?.Invoke(1f, false, Vector2.zero);
    }
}
