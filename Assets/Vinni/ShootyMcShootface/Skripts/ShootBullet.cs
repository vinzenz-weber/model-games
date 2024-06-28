using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to be shot

    public Transform spawnPosition;
    public float bulletSpeed = 20f; // The speed of the bullet
    public float fireRate = 5f; // Time between shots in seconds

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
            nextFireTime = Time.time + Random.Range(1f, 10f);
            bulletSpeed *= 1.2f;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the position and rotation of the empty GameObject
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.Euler(-90f, 0f, 0f));

        // Get the Rigidbody component from the bullet and set its velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPosition.right * bulletSpeed;
        }

        // Destroy the bullet after 10 seconds
        Destroy(bullet, 10f);
    }


}
