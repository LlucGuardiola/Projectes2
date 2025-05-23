using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    [SerializeField] private int[] cards;
    private bool[] values;
    [SerializeField] private GameObject metalDoorSprite;
    private bool doorIsOpen;
    private bool openDoor;

    private void Start()
    {
        doorIsOpen = false;
        openDoor = false;
    }

    private void Update()
    {
        if (doorIsOpen) { return; }

        if (openDoor) 
        { 
            metalDoorSprite.transform.position = new Vector3(metalDoorSprite.transform.position.x, 
                                                             metalDoorSprite.transform.position.y + 0.55f * Time.deltaTime,
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
        if (Input.GetKey("e"))
        {
            if (CheckCards())
            {
                openDoor = true;
                Invoke("DoorHasReachedLimit", 5);
            }
        }
    }

    private void DoorHasReachedLimit()
    {
        GetComponent<Collider2D>().enabled = false;
        doorIsOpen = true;
        openDoor = false;
    }
}
