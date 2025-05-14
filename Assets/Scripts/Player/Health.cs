using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float Life;

    void Update()
    {
        if (Life <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                // SceneManager.LoadScene("BlockoutScene");
                // GetActiveCamera();

                CheckpointManager.Instance.RespawnPlayerAfterReload();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                if (gameObject.GetComponent<CardSpawner>() != null) gameObject.GetComponent<CardSpawner>().InstantiateCard();
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float ammount)
    {
        Life -= ammount;
    }

    Camera GetActiveCamera()
    {
        Camera[] allCameras = Camera.allCameras;

        foreach (Camera cam in allCameras)
        {
            if (cam.isActiveAndEnabled)
            {
                return cam;
            }
        }
        return null; 
    }

}

