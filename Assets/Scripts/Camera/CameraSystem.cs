using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    GameObject cam;
    GameObject player;

    [SerializeField] private float speed;
    [SerializeField] private float distanceToMove;
    [SerializeField] private float camSpeedMultiplier;
    [SerializeField] private LayerMask cameraZoneLayer;

    [HideInInspector] public float HeightIncrease;

    // private bool moveCamera;
    private Vector2 direction;
    private Vector3 playerPosition;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        //moveCamera = true;
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        playerPosition.y += HeightIncrease;

        direction = playerPosition - cam.transform.position;
        direction = direction.normalized;

        Vector3 newPos;
        float distance = Vector2.Distance(cam.transform.position, playerPosition) * camSpeedMultiplier;

        newPos = new Vector3(cam.transform.position.x + direction.x * speed * distance * Time.deltaTime,
                                 cam.transform.position.y + direction.y * speed * distance * Time.deltaTime,
                                 cam.transform.position.z);

        Collider2D[] colliders;

        colliders = Physics2D.OverlapBoxAll(new Vector2(newPos.x, newPos.y), Vector2.one, 0f, cameraZoneLayer);

        if (colliders.Length == 0) 
        {
            newPos.y = cam.transform.position.y;
        }

        if (Vector2.Distance(cam.transform.position, playerPosition) > 2f)
        {
            cam.transform.position = newPos;
        }
    }
}
