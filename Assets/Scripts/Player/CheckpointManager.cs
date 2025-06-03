using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private static Vector3 savedPosition;
    private GameObject player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static void SetCheckpoint(Vector3 position)
    {
        savedPosition = position;
    }

    public void StartRespawn()
    {
        CameraFade.StartFade(true, 1f);
        Invoke("Respawn", 2f / CameraFade.SpeedScale / 2f);
    }

    private void Respawn()
    {
        player.transform.position = savedPosition;
        player.GetComponent<Health>().RestartLife();
    }
}
