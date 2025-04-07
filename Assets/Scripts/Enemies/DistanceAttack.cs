using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void Shoot()
    {
     
    }
}
