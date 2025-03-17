using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public float respawnTime = 3.0f; // Time in seconds before the enemy respawns
    private Vector3 spawnPosition; // Store the spawn position of the enemy

    private void Start()
    {
        spawnPosition = transform.position; // Initialize the spawn position
    }

    // This method is called when the enemy is hit
    public void ReactToHit()
    {
        // If the enemy is inactive, reactivate it immediately before doing anything else
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true); // Reactivate the object immediately
        }

        // Handle the reaction to being hit (e.g., disable, play animation, etc.)
        Debug.Log("Enemy hit, reacting...");
        
        // Start the death and respawn process
        StartCoroutine(Die());    }

    // Coroutine to disable the enemy, wait for respawn time, and then respawn
    private IEnumerator Die()
    {
        // Ensure the object is active before we proceed
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        // Disable the enemy and wait before respawning
        Debug.Log("Disabling enemy...");
        gameObject.SetActive(false); // Disable the enemy when it's "dead"

        // Wait for the respawn time before re-enabling the enemy
        yield return new WaitForSeconds(respawnTime);

        // Respawn the enemy at the original spawn position
        transform.position = spawnPosition;

        // Reactivate the enemy and reset any other necessary states
        gameObject.SetActive(true);
        Debug.Log("Enemy respawned at position: " + spawnPosition);
    }
}
