using UnityEngine;

public class Sword : ItemBase
{
    private void OnGrab()
    {
        Grab();
    }

    public override void Grab()
    {
        player.GetComponent<PlayerAttack>().CanAttack = true;
        Destroy(gameObject);
    }
}
