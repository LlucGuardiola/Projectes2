using UnityEngine;
using System.Collections.Generic;

public class EnableWorldObject : MonoBehaviour
{
    public List<GameObject> triggerObjects;
    public List<GameObject> objectsToActivate;
    public List<bool> startEnabled;

    private void Start()
    {
        if (triggerObjects == null || objectsToActivate == null || startEnabled == null) return;

        if (triggerObjects.Count == 0 || objectsToActivate.Count == 0 || startEnabled.Count == 0) return;

        for (int i = 0; i < triggerObjects.Count; i++)
        {
            objectsToActivate[i].SetActive(startEnabled[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < triggerObjects.Count; i++)
        {
            if (collision.gameObject == triggerObjects[i])
            {
                objectsToActivate[i].SetActive(!startEnabled[i]);

                triggerObjects.RemoveAt(i);
                objectsToActivate.RemoveAt(i);
                startEnabled.RemoveAt(i);

                break;
            }
        }
    }
}
