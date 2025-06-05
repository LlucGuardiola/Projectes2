using UnityEngine;

public class CameraHeightCorrection : MonoBehaviour
{
    [SerializeField] private float newHeightIncrease;
    private GameObject player;
    private GameObject cameraSyst;

    private void Start()
    {
        player = GameObject.Find("Player");
        cameraSyst = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            cameraSyst.GetComponent<CameraSystem>().offset = new Vector2(0, newHeightIncrease);
        }
    }
}
