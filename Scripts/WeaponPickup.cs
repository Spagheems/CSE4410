using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // The weapon that this pickup represents

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Press E to pick up weapon");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Find the WeaponController on the player
            WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
            if (weaponController != null)
            {
                // Replace the current weapon with the new weapon
                weaponController.ReplaceCurrentWeapon(weaponPrefab);
                Destroy(gameObject); // Destroy the pickup object after it's used
            }
            else
            {
                Debug.LogError("WeaponController not found on player!");
            }
        }
    }
}
