using UnityEngine;

public class Enble : MonoBehaviour
{
    public GameObject[] gameobjects;

    void Start()
    {
        foreach (var item in gameobjects)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInventory.PlayerHasSword)
        {
            foreach (var item in gameobjects)
            {
                item.SetActive(true);
            }
        }

        Destroy(gameObject);
    }
}
