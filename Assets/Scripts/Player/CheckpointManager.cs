using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 savedPosition;
    GameObject player;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetCheckpoint(Vector3 position)
    {
        savedPosition = position;
    }

    public void Respawn()
    {
        player.transform.position = savedPosition;
        player.GetComponent<Health>().RestartLife();
    }
}
