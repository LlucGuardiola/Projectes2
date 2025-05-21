using UnityEngine;

public class EnableWorldObject : MonoBehaviour
{
    public GameObject[] triggerObjects;  
    public GameObject[] objectsToActivate; 
    private GameObject player;

    private void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < triggerObjects.Length; i++)
        {
            if (collision.gameObject == triggerObjects[i])
            {
                objectsToActivate[i].SetActive(true);
            }
        }
    }
}
