using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;

    public Vector2 offset;
    private float smoothTime = .15f;
    private Vector2 velocity = Vector2.zero;
    private PlayerMovement playerMovement;

    private float initialZ;

    private void Start()
    {
        player = GameObject.Find("Player");
        initialZ = transform.position.z;
        playerMovement = player.GetComponent<PlayerMovement>(); 
    }

    private void Update()
    {
        offset = new Vector2(playerMovement.LookingForward ? 1 : -1, offset.y);

        Vector2 targetPos = (Vector2)player.transform.position + offset;
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, initialZ);
    }
}
