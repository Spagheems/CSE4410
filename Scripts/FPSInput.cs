using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {
    public float speed = 6.0f;
    public float gravity = 9.81f;

    public AudioClip footstepSound;
    public float footstepInterval = 0.5f;

    private CharacterController charController;
    private float nextFootstepTime = 0f;

    void Start() {
        charController = GetComponent<CharacterController>();
    }

    void Update() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

        HandleFootsteps(deltaX, deltaZ);
    }

    void HandleFootsteps(float deltaX, float deltaZ) {
        if (charController.isGrounded && (Mathf.Abs(deltaX) > 0.01f || Mathf.Abs(deltaZ) > 0.01f)) {
            if (Time.time >= nextFootstepTime) {
                AudioSource.PlayClipAtPoint(footstepSound, transform.position);
                nextFootstepTime = Time.time + footstepInterval;
            }
        }
    }
}