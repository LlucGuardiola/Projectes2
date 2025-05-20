using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public Health playerHealth;
    public SpriteRenderer lifeBarRenderer;
    public Sprite[] lifeSprites; 

    private float maxLife;

    void Start()
    {
        maxLife = playerHealth.Life;
        UpdateLifeDisplay();
    }

    void Update()
    {
        if (lifeBarRenderer.sprite != GetCurrentLifeSprite())
        {
            UpdateLifeDisplay();
        }
    }

    void UpdateLifeDisplay()
    {
        lifeBarRenderer.sprite = GetCurrentLifeSprite();
    }

    Sprite GetCurrentLifeSprite()
    {
        //float lifePercentage = playerHealth.Life / maxLife;
        //int spriteIndex = Mathf.FloorToInt(lifePercentage * (lifeSprites.Length - 1));
        //return lifeSprites[Mathf.Clamp(spriteIndex, 0, lifeSprites.Length - 1)];

        // versió simple: 
        int spriteIndex = (int)playerHealth.Life;
        return lifeSprites[spriteIndex];

    }
}
