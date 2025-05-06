using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 savedPosition;
    private GameObject savedCamera;
    private string lastCameraName;
    private string sceneName;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetCheckpoint(Vector3 position, GameObject camera)
    {
        savedPosition = position;
        lastCameraName = camera.name;
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Checkpoint guardado en: " + position);
    }

    public void RespawnPlayerAfterReload()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneReloaded;
    }

    private void OnSceneReloaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = savedPosition;

            GameObject cam = GameObject.Find(lastCameraName);
            if (cam != null)
            {
                cam.SetActive(true);
            }
        }

        SceneManager.sceneLoaded -= OnSceneReloaded; 
    }

}
