using UnityEngine;

public class ShootLogic_Shooty : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to be shot
    public float bulletSpeed = 20f; // The speed of the bullet
    public float fireRate = 2f; // Time between shots in seconds

    private float nextFireTime = 0f;

    void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the position and rotation of the empty GameObject
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Get the Rigidbody component from the bullet and set its velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = -transform.forward * bulletSpeed;
        }
    }
}
