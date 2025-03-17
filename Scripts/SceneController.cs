using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject[] enemyPrefabs; // Use an array to store multiple enemy types
    [SerializeField] private Transform[] spawnPoints; // Array of spawn locations
    [SerializeField] private int initialEnemyCount = 3; // Enemies to start with
    [SerializeField] private float respawnDelay = 3.0f; // Delay before spawning new enemies

    private void Start()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned in SceneController!");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in SceneController!");
            return;
        }

        SpawnEnemies(initialEnemyCount);
    }

    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemyInstance = Instantiate(enemyPrefabs[randomPrefabIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
        EnemyAI enemyScript = enemyInstance.GetComponent<EnemyAI>();

        if (enemyScript != null)
        {
            enemyScript.OnDeath += HandleEnemyDeath; // Subscribe to death event
        }
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        StartCoroutine(RespawnEnemy(enemy));
    }

    private IEnumerator RespawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(respawnDelay);
        Destroy(enemy); // Cleanup old enemy
        SpawnEnemy(); // Spawn a new one
    }
}
