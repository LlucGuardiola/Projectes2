using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    private bool[] values;
    [SerializeField] private int[] cards;
    [SerializeField] private GameObject[] cardSprites;
    [SerializeField] private GameObject metalDoorSprite;
    private bool doorIsOpen;
    private bool openDoor;
    private UnityEngine.Color[] initialColors;

    private void Start()
    {
        doorIsOpen = false;
        openDoor = false;
        initialColors = new UnityEngine.Color[cardSprites.Length];

        for (int i = 0; i < cardSprites.Length; i++)
        {
            initialColors[i] = cardSprites[i].GetComponent<SpriteRenderer>().color;
        }
    }

    private void Update()
    {
        if (doorIsOpen) { return; }

        if (openDoor) 
        { 
            metalDoorSprite.transform.position = new Vector3(metalDoorSprite.transform.position.x, 
                                                             metalDoorSprite.transform.position.y + 1.35f * Time.deltaTime,
                                                             metalDoorSprite.transform.position.z);
        }
    }

    private bool CheckCards()
    {
        values = new bool[cards.Length];

        for (int i = 0; i < values.Length; i++)
        {
            values[i] = false;
        }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(CheckCards())
        {
            foreach (var card in cardSprites)
            {
                SpriteRenderer cardSpriteRenderer = card.GetComponent<SpriteRenderer>();

                UnityEngine.Color newColor = new UnityEngine.Color(1f, 1f, 1f, 1f); 
                cardSpriteRenderer.color = newColor;
            }

            if (Input.GetKey("e"))
            {
                openDoor = true;
                Invoke("DoorHasReachedLimit", 1.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (doorIsOpen) return;

        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardSprites[i].GetComponent<SpriteRenderer>().color = initialColors[i];
        }
    }

    private void DoorHasReachedLimit()
    {
        GetComponent<Collider2D>().enabled = false;
        doorIsOpen = true;
        openDoor = false;
    }
}
