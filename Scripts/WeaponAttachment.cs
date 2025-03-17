using UnityEngine;

public class WeaponAttachment : MonoBehaviour
{
    public GameObject weaponPrefab; // Assign your weapon prefab
    public Transform cameraTransform; // Assign the Main Camera in Inspector
    public Vector3 weaponOffset = new Vector3(0, -0.1f, 0.2f); // Optional position offset
    public Vector3 rotationOffset = Vector3.zero; // Optional rotation offset
    public Transform handPosition; // Drag the player's hand bone or empty GameObject

    private static GameObject currentWeapon; // Static to persist and avoid duplicates
    private static WeaponAttachment instance; // Singleton to prevent multiple controllers

    void Awake()
    {
        // Ensure only one WeaponAttachment script exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate scripts
            return;
        }
        instance = this;

        // Make sure the weapon only gets attached once
        if (currentWeapon == null)
        {
            AttachWeapon();
        }
    }

    void AttachWeapon()
    {
        if (weaponPrefab == null || cameraTransform == null)
        {
            Debug.LogError("WeaponPrefab or CameraTransform is not assigned!");
            return;
        }

        if (currentWeapon != null)
        {
            Debug.LogWarning("Weapon already attached!");
            return;
        }

        // Instantiate and attach the weapon
        currentWeapon = Instantiate(weaponPrefab, cameraTransform.position, cameraTransform.rotation);
        currentWeapon.transform.SetParent(cameraTransform);

        // Apply offset for position and rotation
        currentWeapon.transform.localPosition = weaponOffset;
        currentWeapon.transform.localRotation = Quaternion.Euler(rotationOffset);

        DontDestroyOnLoad(currentWeapon); // Persist the weapon across scenes
    }
}
