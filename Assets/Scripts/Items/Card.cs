using UnityEngine;

public class Card : ItemBase
{
    public int Index = 0;

    private void OnGrab()
    {
        if (CanGrab()) { Grab(); }
    }

    public override void Grab()
    {
        PlayerInventory.AddCard(Index);
        Destroy(gameObject);
    }
}
