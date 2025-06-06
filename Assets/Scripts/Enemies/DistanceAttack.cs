using UnityEngine;
using SmallHedge.SoundManager;

public class DistanceAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    [SerializeField] private bool reload; //activar rafagas
    [SerializeField] private float reloadDuration; //cooldown entre rafagas
    [SerializeField] private float shootsToReload; //balas que dispara para que recargue las rafagas

    private bool canShoot;
    private bool firstShoot;
    private float bulletCounter;
    private float fireCooldown;
    [HideInInspector] public GameObject player;
    private Animator animator;

    void Start()
    {
        canShoot = true;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        firstShoot= true;
    }

    void Update()
    {
        if (!canShoot) return;
        if (GetComponent<KnockbackFeedback>().IsKnocked) return;
        if (GetComponent<Enemy>().IsDead) return;
        if (!firstShoot) fireCooldown -= Time.deltaTime;
        if (GetComponent<VisionDetection>().CheckLineOfSight() == 1) return;

        if (bulletCounter == shootsToReload)
        {
            if (reload) animator.SetBool("isReloading?", true);
            canShoot = false;
            Invoke("EnableShoot", reloadDuration);
            bulletCounter = 0;
        }
        if (GetComponent<Enemy>().InRange && GetComponent<Enemy>().DistanceAttack) firstShoot = false;
        if (fireCooldown <= 0f && GetComponent<Enemy>().InRange && GetComponent<Enemy>().DistanceAttack)
        {
            Shoot(player.transform);
            fireCooldown = fireRate;
            if (reload) bulletCounter += 1;
        }
    }

    private void Shoot(Transform player)
    {
        if (reload) animator.SetBool("isReloading?", false);

        SoundManager.PlaySound(SoundType.DistanceAttack);

        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(player.position);
    }

    private void EnableShoot()
    {
        canShoot=true;
    }
}
