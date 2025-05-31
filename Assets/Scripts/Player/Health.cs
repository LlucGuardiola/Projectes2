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
                if (gameObject.GetComponent<Enemy>().IsDead == true) return;

                if (gameObject.GetComponent<CardSpawner>() != null)
                    gameObject.GetComponent<CardSpawner>().InstantiateCard();

                gameObject.GetComponent<Enemy>().IsDead = true;
                Invoke("Remove", 1f);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (gameObject.CompareTag("Player")) 
        { 
            if (GetComponent<GroundDash>().IsGroundDashing) return;
            CameraShake.Instance.StartShake(0.2f, 0.1f);
        }

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

