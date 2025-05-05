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
                //SceneManager.LoadScene("BlockoutScene");
               // GetActiveCamera();

                CheckpointManager.Instance.RespawnPlayerAfterReload();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void TakeDamage(float ammount)
    {
        Life -= ammount;
        Debug.Log("-1");
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

