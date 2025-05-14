using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Color color;
    [SerializeField] private int index;

    public void InstantiateCard()
    {
        GameObject card = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        card.GetComponent<SpriteRenderer>().color = color;
        card.GetComponent<Card>().Index = index;
    }
}
