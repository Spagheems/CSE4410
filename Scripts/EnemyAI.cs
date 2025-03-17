using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f;
    private Transform player;
    private bool isAlive = true;
    private Vector3 spawnPoint;

    private Rigidbody rb; // Reference to the Rigidbody component

    public delegate void DeathEvent(GameObject enemy);
    public event DeathEvent OnDeath; // Event to notify SceneController

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>(); // Get Rigidbody
        spawnPoint = transform.position; // Store spawn point
    }

    private void Update()
    {
        if (isAlive && player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player);
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;

        if (!alive)
        {
            OnDeath?.Invoke(gameObject); // Notify SceneController

            // Reset physics to stop enemy from flying away
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Disable physics during respawn
            }

            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        gameObject.SetActive(false); // Hide enemy
        yield return new WaitForSeconds(3.0f); // Wait before respawning
        transform.position = spawnPoint;

        // Reset physics after respawn
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        gameObject.SetActive(true);
        isAlive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SetAlive(false);
            Destroy(collision.gameObject); // Destroy projectile on hit
        }
    }
}
