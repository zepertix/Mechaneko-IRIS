using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    [Tooltip("Projectiles per second")]
    public float fireRate = 5f;

    [Range(0f, 1f)]
    public float shootVolume = 1f;

    //2-14-26 start
    public AudioClip shootSound;
    private AudioSource audioSource;
    //2-14-26 end

    private float fireCooldown = 0f;
    private Vector3 direction = Vector3.right;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    //2-14-26 start
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found. Adding one automatically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    //2-14-26 end

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

        //2-14-26 start
        Debug.Log("Fire() running");

        Debug.Log("audioSource = " + audioSource);
        Debug.Log("shootSound = " + shootSound);

        if (audioSource != null && shootSound != null)
        {
            Debug.Log("Playing shoot sound");
            audioSource.PlayOneShot(shootSound, shootVolume);
        }
        else
        {
            Debug.LogWarning("AudioSource or ShootSound is NULL");
        }
        //2-14-26 end

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