using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float horizontalThreshold = 2f;
    [SerializeField] private float verticalThreshold = 1.5f;
    [SerializeField] private float camSpeedMultiplier = 2f;
    [SerializeField] private LayerMask cameraZoneLayer;

    [HideInInspector] public float HeightIncrease;

    private GameObject cam;
    private GameObject player;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.y += HeightIncrease;

        Vector3 camPosition = cam.transform.position;

        Vector3 offset = playerPosition - camPosition;

        bool moveX = Mathf.Abs(offset.x) > horizontalThreshold;
        bool moveY = Mathf.Abs(offset.y) > verticalThreshold;

        Vector3 targetPosition = camPosition;

        if (moveX) targetPosition.x = playerPosition.x;
        if (moveY) targetPosition.y = playerPosition.y;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(targetPosition.x, targetPosition.y), Vector2.one, 0f, cameraZoneLayer);

        if (colliders.Length == 0)
        {
            targetPosition.y = camPosition.y;
        }

        cam.transform.position = Vector3.Lerp(camPosition, 
                                              new Vector3(targetPosition.x, targetPosition.y, camPosition.z), 
                                              Time.deltaTime * speed * camSpeedMultiplier);
    }
}
