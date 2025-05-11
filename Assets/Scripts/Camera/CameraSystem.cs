using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    GameObject cam;
    GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToMove;
    [SerializeField] private LayerMask cameraZoneLayer;
    // private bool moveCamera;
    private Vector2 direction;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        //moveCamera = true;
    }

    private void Update()
    {
        direction = player.transform.position - cam.transform.position;
        direction = direction.normalized;

        Vector3 newPos;
        float distance = 1f; //(cam.transform.position - player.transform.position).magnitude * 0.1f;

        if (player.GetComponent<PlayerJump>().IsTouchingGround)
        {
            newPos = cam.transform.position + (Vector3)direction * speed * distance * Time.deltaTime;
        }
        else
        {
            newPos = new Vector3(cam.transform.position.x + direction.x * speed * distance * Time.deltaTime,
                                 cam.transform.position.y + direction.y * speed / 2 * distance * Time.deltaTime,
                                 cam.transform.position.z);
        }

        Collider2D[] colliders;

        colliders = Physics2D.OverlapBoxAll(new Vector2(newPos.x, newPos.y), Vector2.one, 0f, cameraZoneLayer);

        if (colliders.Length == 0) 
        {
            newPos.y = cam.transform.position.y;
        }

        if (Vector2.Distance(cam.transform.position, player.transform.position) > 1f)
        {
            cam.transform.position = newPos;
        }
    }
}
