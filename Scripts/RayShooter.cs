using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private GameObject projectilePrefab; // Custom projectile model prefab
    [SerializeField] private float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] private float spawnDistance = 1.0f; // Distance in front of the player to spawn the projectile

    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    // Shoots the projectile forward from the camera
    private void ShootProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned!");
            return;
        }

        // Calculate spawn position slightly ahead of the camera to avoid hitting the player
        Vector3 spawnPosition = cam.transform.position + cam.transform.forward * spawnDistance;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, cam.transform.rotation);

        if (projectile == null)
        {
            Debug.LogError("Projectile failed to instantiate!");
            return;
        }

        Debug.Log($"Projectile spawned: {projectile.name}");

        // Apply velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = cam.transform.forward * projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile prefab is missing a Rigidbody!");
        }
    }

    // Draw crosshair in the middle of the screen
    private void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
