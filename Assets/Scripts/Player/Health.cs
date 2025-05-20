using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float Life;
    public event System.Action OnLifeChanged; 

    void Update()
    {
        if (Life <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                CheckpointManager.Instance.RespawnPlayerAfterReload();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                if (gameObject.GetComponent<CardSpawner>() != null)
                    gameObject.GetComponent<CardSpawner>().InstantiateCard();
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        Life -= amount;
        OnLifeChanged?.Invoke(); 
    }
}

