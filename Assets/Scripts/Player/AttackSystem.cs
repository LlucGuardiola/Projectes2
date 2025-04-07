using System;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static Action<float> OnAttackDone;

    public void OnAttack()
    {
        if (PauseLogic.IsPaused) return;
        
        
        OnAttackDone?.Invoke(1f);
    }
}
