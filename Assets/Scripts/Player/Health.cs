using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public float Life;
    public bool isPlayer;
    public Image[] hearts;
    public Sprite redHeart;
    public Sprite emptyHeart;

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

    public void TakeDamage(float amount)
    {
        Life -= amount;
        Debug.Log("-1");
        if (isPlayer)
        {
            UpdateHearts();
        }
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
    public void UpdateHearts()
    { 
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Life)
            {
                hearts[i].sprite = redHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }


}

