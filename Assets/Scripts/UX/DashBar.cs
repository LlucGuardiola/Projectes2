using UnityEngine;

public class DashBar : MonoBehaviour
{
    public Dash playerDash; // Referencia al script Dash
    public SpriteRenderer cooldownRenderer; // Renderer para mostrar los sprites
    public Sprite[] cooldownSprites; // Sprites para los estados de cooldown (0-4)

    private float cooldownDuration = 4f; // Duración total del cooldown
    private float currentCooldown; // Tiempo restante de cooldown
    private bool isOnCooldown; // Indica si está en cooldown

    void Start()
    {
        // Suscribirse al evento de dash
        Dash.OnDashEnd += StartCooldown;
        currentCooldown = 0f;
        UpdateCooldownDisplay();
    }

    void OnDestroy()
    {
        // Importante: Desuscribirse del evento
        Dash.OnDashEnd -= StartCooldown;
    }

    void Update()
    {
        if (isOnCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                currentCooldown = 0f;
                isOnCooldown = false;
            }

            UpdateCooldownDisplay();
        }
    }

    void StartCooldown(float duration, bool success, Vector2 direction)
    {
        if (success) // Solo activar cooldown si el dash fue exitoso
        {
            currentCooldown = cooldownDuration;
            isOnCooldown = true;
            UpdateCooldownDisplay();
        }
    }

    void UpdateCooldownDisplay()
    {
        // Calcula el índice basado en el tiempo restante
        int spriteIndex = Mathf.CeilToInt((currentCooldown / cooldownDuration) * (cooldownSprites.Length - 1));
        spriteIndex = Mathf.Clamp(spriteIndex, 0, cooldownSprites.Length - 1);
        cooldownRenderer.sprite = cooldownSprites[spriteIndex];
    }
}
