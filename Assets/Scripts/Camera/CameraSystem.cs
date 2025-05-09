using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    GameObject cam;
    GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToMove;
    [SerializeField] private LayerMask cameraZoneLayer;
    private bool moveCamera;
    private Vector2 direction;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        moveCamera = true;
    }

    private void Update()
    {
        direction = player.transform.position - cam.transform.position;
        direction = direction.normalized;

        float newSpeed = speed;

        if (!player.GetComponent<PlayerJump>().IsTouchingGround)
        {
            newSpeed = speed / 3;
        }

        Vector3 newPos = cam.transform.position + (Vector3)direction * newSpeed * Time.deltaTime;

        Collider2D[] colliders;

        colliders = Physics2D.OverlapBoxAll(new Vector2(newPos.x, newPos.y), Vector2.one, 0, cameraZoneLayer);
        
        if (colliders.Length == 0) 
        {
            Debug.Log("biahdw");
            newPos.y = cam.transform.position.y;
        }

        if (moveCamera && (player.transform.position - cam.transform.position).sqrMagnitude > 1f)
        {
            cam.transform.position = newPos;
        }
    }
}
