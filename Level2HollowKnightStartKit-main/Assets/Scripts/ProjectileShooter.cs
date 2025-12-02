using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection; 
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectilePrefab) as GameObject;

        newProjectile.transform.position = spawnPoint.position;

        ProjectileController newProjectileController = newProjectile.GetComponent<ProjectileController>();

        if (newProjectileController != null)
        {
            newProjectileController.Setup(direction);
        }
        else
        {
            Debug.LogWarning("Projectile is missing a projectile controller, idiot");  
          
        }
    }
//deleted start and update, this one does niether!
}
