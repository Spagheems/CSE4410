using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilAmount = 2.0f;      // Amount of vertical camera recoil (kick)
    public float horizontalRecoil = 1.0f;  // Horizontal sway during recoil
    public float verticalRecoil = 1.0f;    // Vertical sway during recoil
    public float recoilSpeed = 5.0f;       // Speed of recoil movement
    public float returnSpeed = 6.0f;       // Speed at which the camera returns to normal

    [Header("Shake Settings")]
    public float shakeIntensity = 0.1f;    // Camera shake intensity
    public float shakeDuration = 0.1f;     // How long the shake lasts

    private Quaternion originalRotation;   // To store the original camera rotation
    private Quaternion currentRecoilRotation;  // Current recoil applied to the camera
    private Quaternion targetRecoilRotation;   // Target recoil to gradually reach
    private float shakeTimer;          // Timer for camera shake

    void Start()
    {
        // Initialize original rotation and ensure it's valid
        originalRotation = transform.localRotation;
        currentRecoilRotation = Quaternion.identity;  // Set initial recoil to zero
        targetRecoilRotation = Quaternion.identity;   // Set target recoil to zero
    }

    void Update()
    {
        // Gradually apply recoil over time (smooth transition)
        currentRecoilRotation = Quaternion.Lerp(currentRecoilRotation, targetRecoilRotation, Time.deltaTime * recoilSpeed);

        // Ensure the current recoil rotation is valid before applying it
        if (!QuaternionIsValid(currentRecoilRotation))
        {
            currentRecoilRotation = Quaternion.identity; // Reset if invalid
        }

        // Apply the recoil rotation to the camera
        transform.localRotation = originalRotation * currentRecoilRotation;

        // Gradually return to original position (camera returns to neutral)
        targetRecoilRotation = Quaternion.Lerp(targetRecoilRotation, Quaternion.identity, Time.deltaTime * returnSpeed);

        // Apply camera shake if the shake timer is still active
        if (shakeTimer > 0)
        {
            // Apply shake in all directions (vertical, horizontal, and roll)
            transform.localRotation *= Quaternion.Euler(
                Random.Range(-shakeIntensity, shakeIntensity), // Vertical shake
                Random.Range(-shakeIntensity, shakeIntensity), // Horizontal shake
                Random.Range(-shakeIntensity, shakeIntensity)  // Roll shake for added variety
            );
            shakeTimer -= Time.deltaTime;
        }
    }

    // Public method to trigger recoil when the player fires
    public void RecoilFire()
    {
        // Apply recoil to each axis using Quaternions
        targetRecoilRotation *= Quaternion.Euler(
            -recoilAmount,  // Negative for downward vertical recoil (X-axis)
            Random.Range(-horizontalRecoil, horizontalRecoil),  // Random horizontal recoil (Y-axis)
            Random.Range(-verticalRecoil, verticalRecoil)   // Random vertical recoil (Z-axis)
        );

        // Start the camera shake
        shakeTimer = shakeDuration;
    }

    // Utility function to check if a quaternion is valid
    private bool QuaternionIsValid(Quaternion quat)
    {
        return !float.IsNaN(quat.x) && !float.IsNaN(quat.y) && !float.IsNaN(quat.z) && !float.IsNaN(quat.w);
    }
}
