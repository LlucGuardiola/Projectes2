using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int life; 
    void Start()
    {
        life = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) Destroy(this);
    }
}
