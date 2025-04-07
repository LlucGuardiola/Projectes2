using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float detectionRange = 5f;
    private float fireCooldown;
    private VisionDetection detection;
    public GameObject player;
    void Start()
    {
        detection = GetComponent<VisionDetection>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      fireCooldown -= Time.deltaTime;
     // if(detection.DetectedPlayer != null && fireCooldown <= 0f) 
      //{
   //         Shoot(player.transform);
     //       fireCooldown = fireRate; 
     // }
    }

    private void Shoot(Transform player)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(player.position);
        Debug.Log("si");
    }
}
