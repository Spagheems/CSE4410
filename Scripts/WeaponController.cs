using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject primaryWeaponPrefab;  // Reference to the primary weapon prefab
    public Transform weaponHolder;          // Location where the weapon should be held
    public AudioClip fireSound;             // Reference to the firing sound effect

    [Header("Aiming Positions")]
    public Transform hipPosition;
    public Transform adsPosition;

    [Header("Settings")]
    public float aimSpeed = 10.0f;

    private GameObject currentWeapon;       // Reference to the current weapon in hand
    private AudioSource audioSource;        // Audio source for firing sounds

    void Start()
    {
        // Ensure we have an AudioSource attached to the player or weapon
        audioSource = GetComponentInChildren<AudioSource>();

        EquipPrimaryWeapon();
    }

    void Update()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("Weapon is missing! Trying to reattach.");
            EquipPrimaryWeapon();
            return;
        }

        HandleADS();

        // Fire weapon when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        // Play the firing sound effect
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        // Add any other logic for firing the weapon (like spawning projectiles, etc.)
        Debug.Log("Weapon Fired!");
    }

    // Method to replace the current weapon with a new weapon (when picked up)
    public void ReplaceCurrentWeapon(GameObject newWeaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // Destroy the old weapon
        }

        // Equip the new weapon
        currentWeapon = Instantiate(newWeaponPrefab, weaponHolder);
        SetupWeapon(currentWeapon);
        Debug.Log("New weapon equipped!");
    }

    void EquipPrimaryWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (primaryWeaponPrefab != null)
        {
            currentWeapon = Instantiate(primaryWeaponPrefab, weaponHolder);
            SetupWeapon(currentWeapon);
            Debug.Log("Primary weapon equipped!");
        }
        else
        {
            Debug.LogError("Primary weapon prefab is not assigned!");
        }
    }

    void SetupWeapon(GameObject weapon)
{
    if (weapon == null) return;
    
    weapon.transform.localPosition = Vector3.zero;
    weapon.transform.localRotation = Quaternion.Euler(0, 180, 0); // Rotates the weapon 180° on Y-axis to correct its direction
}

    void HandleADS()
    {
        if (hipPosition == null || adsPosition == null || weaponHolder == null)
        {
            Debug.LogError("One of the position references is missing!");
            return;
        }

        bool isAiming = Input.GetMouseButton(1);

        Vector3 targetPosition = isAiming ? adsPosition.localPosition : hipPosition.localPosition;
        Quaternion targetRotation = isAiming ? adsPosition.localRotation : hipPosition.localRotation;

        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, targetPosition, Time.deltaTime * aimSpeed);
        weaponHolder.localRotation = Quaternion.Lerp(weaponHolder.localRotation, targetRotation, Time.deltaTime * aimSpeed);
    }
}
