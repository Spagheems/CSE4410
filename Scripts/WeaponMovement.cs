using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothness = 8.0f;

    [Header("Bobbing Settings")]
    public float bobSpeed = 8.0f;
    public float bobAmount = 0.02f;

    [Header("Rotation Settings")]
    public float tiltAmount = 4.0f;
    public float maxTilt = 8.0f;

    [Header("Recoil Settings")]
    public float recoilAmount = 0.05f;
    public float recoilRecoverySpeed = 8.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float bobTimer = 0f;
    private Vector3 recoilOffset = Vector3.zero;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        ApplySway();
        ApplyBobbing();
        ApplyRecoilRecovery();
    }

    void ApplySway()
    {
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;

        mouseX = Mathf.Clamp(mouseX, -maxSwayAmount, maxSwayAmount);
        mouseY = Mathf.Clamp(mouseY, -maxSwayAmount, maxSwayAmount);

        Vector3 targetPosition = initialPosition + new Vector3(mouseX, mouseY, 0) + recoilOffset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * swaySmoothness);

        float tiltZ = -mouseX * tiltAmount;
        float tiltX = mouseY * tiltAmount;

        tiltZ = Mathf.Clamp(tiltZ, -maxTilt, maxTilt);
        tiltX = Mathf.Clamp(tiltX, -maxTilt, maxTilt);

        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x + tiltX,
                                                     initialRotation.eulerAngles.y,
                                                     initialRotation.eulerAngles.z + tiltZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swaySmoothness);
    }

    void ApplyBobbing()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;
            Vector3 bobPosition = initialPosition + new Vector3(0, bobOffset, 0) + recoilOffset;
            transform.localPosition = Vector3.Lerp(transform.localPosition, bobPosition, Time.deltaTime * swaySmoothness);
        }
        else
        {
            bobTimer = 0;
        }
    }

    public void ApplyRecoil(Vector3 recoil)
    {
        recoilOffset += recoil * recoilAmount;
    }

    void ApplyRecoilRecovery()
    {
        recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, Time.deltaTime * recoilRecoverySpeed);
    }
}
