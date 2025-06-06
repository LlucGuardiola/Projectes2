using UnityEngine;
using SmallHedge.SoundManager;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Sprite sprite;
    [SerializeField] private Color color;
    [SerializeField] private int index;

    public void InstantiateCard()
    {
        GameObject card = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        SoundManager.PlaySound(SoundType.DropCarta);
        card.GetComponent<SpriteRenderer>().color = color;        
        card.GetComponent<SpriteRenderer>().sprite = sprite;
        card.GetComponent<Card>().Index = index;
    }
}
