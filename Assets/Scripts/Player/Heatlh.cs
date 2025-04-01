using UnityEngine;

public class Health : MonoBehaviour
{
    public float Life;

    void Update()
    {
        if (Life <= 0) Destroy(this.gameObject);
    }

    public void TakeDamage(float ammount)
    {
        Life -= ammount;
    }
}

