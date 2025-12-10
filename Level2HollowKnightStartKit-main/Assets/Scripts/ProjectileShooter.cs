using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    [Tooltip("Projectiles per second")]
    public float fireRate = 5f;

    private float fireCooldown = 0f;
    private Vector3 direction = Vector3.right;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    void Update()
    {
        // Count down the cooldown timer
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    public void TryFire()
    {
        if (fireCooldown <= 0f)
        {
            Debug.Log("TryFire called!"); // <-- Add this line to see if firing happens
            if (projectilePrefab == null || spawnPoint == null)
            {
                Debug.LogWarning("ProjectileShooter missing prefab or spawn point!");
                return;
            }
            Fire();
            fireCooldown = 1f / fireRate;
        }
    }

    private void Fire()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);

        newProjectile.transform.position = spawnPoint.position;

        ProjectileController controller = newProjectile.GetComponent<ProjectileController>();
        if (controller != null)
        {
            controller.Setup(direction);
        }
        else
        {
            Debug.LogWarning("Projectile missing a ProjectileController component.");
        }
    }
}