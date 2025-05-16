using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    [SerializeField] private int[] cards;
    private bool[] values;

    private void Update()
    {
        values = new bool[cards.Length];
        
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = false;
        }
    }
    private bool CheckCards()
    {
        foreach (var card in PlayerInventory.PlayerCards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (!values[i])
                {
                    if (card == cards[i])
                    {
                        values[i] = true;
                        break;
                    }
                }
            }
        }

        foreach (var value in values)
        {
            if (!value) return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckCards())
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
