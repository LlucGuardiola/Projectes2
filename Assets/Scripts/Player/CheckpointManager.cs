using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 savedPosition;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetCheckpoint(Vector3 position)
    {
        savedPosition = position;
    }

    public void Respawn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = savedPosition;
    }
}
