using UnityEngine;

public class CarShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;   // where the cube spawns
    public float shootForce = 1200f;
    public float fireRate = 0.3f;

    private float nextFireTime;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject cube = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * shootForce);
    }
}
