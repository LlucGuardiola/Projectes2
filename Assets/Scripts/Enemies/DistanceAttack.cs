using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private float fireCooldown;
    [HideInInspector] public GameObject player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f && GetComponent<Enemy>().InRange && GetComponent<Enemy>().DistanceAttack)
        {
            Shoot(player.transform);
            fireCooldown = fireRate;
        }
    }

    private void Shoot(Transform player)
    {
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(player.position);
    }
}
