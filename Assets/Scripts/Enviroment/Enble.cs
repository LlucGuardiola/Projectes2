using UnityEngine;

public class EnableWhenHasSword : MonoBehaviour
{
    public GameObject[] gameobjects;

    void Start()
    {
        foreach (var item in gameobjects)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerInventory.PlayerHasSword)
        {
            foreach (var item in gameobjects)
            {
                item.SetActive(true);
            }

            // Destruir només quan ja hem activat tot
            Destroy(gameObject);
        }
    }
}
