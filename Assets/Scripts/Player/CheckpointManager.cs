using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private static Vector3 savedPosition;
    private GameObject player;
    public static bool IsDead;
    [SerializeField] private GameObject greenLight;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        IsDead = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        greenLight.SetActive(false);
    }

    public static void SetCheckpoint(Vector3 position)
    {
        savedPosition = position;
    }

    public void StartRespawn()
    {
        if (IsDead) return;

        IsDead = true;
        greenLight.SetActive(true);
        player.GetComponent<Animator>().SetBool("IsDead", true);
        Invoke("StartFade", 0.6f);
    }

    private void StartFade()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        CameraFade.StartFade(true, 1f);
        Invoke("Respawn", 1);
    }
    private void Respawn()
    {
        greenLight.SetActive(false);
        player.GetComponent<Animator>().SetBool("IsDead", false);
        player.transform.position = savedPosition;
        player.GetComponent<Health>().RestartLife();
        IsDead = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
