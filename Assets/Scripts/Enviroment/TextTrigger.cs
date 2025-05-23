using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject worldText; 

    private void Start()
    {
        worldText.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            worldText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            worldText.SetActive(false);
        }
    }
}

