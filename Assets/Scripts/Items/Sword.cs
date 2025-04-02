using UnityEngine;

public class Sword : ItemBase
{
    private void OnGrab()
    {
        if (CanGrab()) { Grab(); }
    }

    public override void Grab()
    {
        player.GetComponent<PlayerAttack>().CanAttack = true;
        Destroy(gameObject);
    }
}
