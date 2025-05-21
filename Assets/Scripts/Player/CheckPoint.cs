using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player) CheckpointManager.Instance.SetCheckpoint(transform.position);
    }
}
