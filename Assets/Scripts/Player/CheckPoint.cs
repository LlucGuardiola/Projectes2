using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject torch;

    private void Start()
    {
        player = GameObject.Find("Player");

        if (torch != null)
        {
            torch.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) CheckpointManager.SetCheckpoint(transform.position);

        if (torch != null)
        {
            torch.SetActive(true);
        }
    }
}
