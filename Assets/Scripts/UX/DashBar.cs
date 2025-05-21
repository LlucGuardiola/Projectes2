using UnityEngine;

public class DashBar : MonoBehaviour
{
    public Dash playerDash;
    public SpriteRenderer cooldownRenderer;
    public Sprite[] cooldownSprites; 
    private GameObject player;

    private float cooldownDuration; 
    private float currentCooldown; 
    private bool isOnCooldown; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cooldownDuration = player.GetComponent<SpecialAbility>().dashCooldown;

        Dash.OnDashEnd += StartCooldown;
        currentCooldown = 0f;
        UpdateCooldownDisplay();
    }

    void OnDestroy()
    {
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
        if (success) 
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
