using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float Life;
    private float startingLife;

    private void Start()
    {
        startingLife = Life;
    }
    void Update()
    {
        if (Life <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                CheckpointManager.Instance.Respawn();
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                if (gameObject.GetComponent<CardSpawner>() != null)
                    gameObject.GetComponent<CardSpawner>().InstantiateCard();

                gameObject.GetComponent<Enemy>().IsDead = true;
                Debug.Log("dead");
            }

            Invoke("Remove", 1f);
        }
    }

    public void TakeDamage(float amount)
    {
        Life -= amount;
    }

    private void Remove()
    {
        Destroy(gameObject);
    }

    public void RestartLife()
    {
        Life = startingLife;
    }
}

