using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected GameObject player = GameObject.FindWithTag("Player");

    protected void CanGrab()
    {

    }

    public virtual void Grab()
    {

    }
}
