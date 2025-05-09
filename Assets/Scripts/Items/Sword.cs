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
        Enemy.PlayerHasSword = true;
        Destroy(gameObject);
    }
}
