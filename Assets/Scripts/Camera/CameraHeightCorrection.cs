using UnityEngine;

public class CameraHeightCorrection : MonoBehaviour
{
    [SerializeField] private float newHeightIncrease;
    private GameObject player;
    private GameObject cameraSyst;

    private void Start()
    {
        player = GameObject.Find("Player");
        cameraSyst = GameObject.Find("CameraSystem");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            cameraSyst.GetComponent<CameraSystem>().HeightIncrease = newHeightIncrease;
        }
    }
}
