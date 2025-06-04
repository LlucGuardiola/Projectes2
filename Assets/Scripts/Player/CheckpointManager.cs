using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private static Vector3 savedPosition;
    private GameObject player;
    public static bool Respawning;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        Respawning = false;
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
        if (Respawning) return;
        CameraFade.StartFade(true, 1f);
        Invoke("Respawn", 2f / CameraFade.SpeedScale / 2f);
        //Respawning = true; 
        
    }

    private void Respawn()
    {
        player.transform.position = savedPosition;
        player.GetComponent<Health>().RestartLife();
        //Respawning = false;
    }
}
