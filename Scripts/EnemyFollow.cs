using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;      // Player's transform (drag the player here in the inspector)
    public float moveSpeed = 3.0f; // Movement speed of the enemy
    public float rotationSpeed = 5.0f; // How fast the enemy turns toward the player

    void Update()
    {
        if (player == null) return;

        // Face the player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Ignore vertical rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move toward the player
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }
}
