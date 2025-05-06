using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class KnockbackFeedback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D rb;
    private bool isKnocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (!isKnocked)
        {
            isKnocked = true;
            rb.linearVelocity = Vector2.zero; // Resetea velocidad anterior
            rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
            Invoke("ResetKnockback", knockbackDuration);
        }
    }

    void ResetKnockback()
    {
        isKnocked = false;
        rb.linearVelocity = Vector2.zero;
    }

}
