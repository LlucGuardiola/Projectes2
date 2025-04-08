using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float Life;

    void Update()
    {
        if (Life <= 0)
        {
            Destroy(gameObject);

            if (gameObject.CompareTag("Player"))
            {
              SceneManager.LoadScene("BlockoutScene");
            }
        }
    }

    public void TakeDamage(float ammount)
    {
        Life -= ammount;
        Debug.Log("-1");
    }
}

