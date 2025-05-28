using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private SpriteRenderer target;
    void Start()
    {
        target = GetComponent<SpriteRenderer>();
        target.enabled = false;
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hola");
        target.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target.enabled = false;

    }
}
