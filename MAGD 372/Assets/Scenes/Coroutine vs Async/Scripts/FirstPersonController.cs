using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] [Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] private bool cursorLock = true;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float speed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private float velocityY;
    [SerializeField] private bool isGrounded;

    private float cameraCap;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private CharacterController controller;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;
    private Vector3 groundCheck;

    void Awake() {
        controller = GetComponent<CharacterController>();
        if (cursorLock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update() {
        UpdateMouse();
        UpdateMove();
    }

    private void FixedUpdate() {
        groundCheck = transform.position + groundCheckOffset;
    }

    void UpdateMouse() {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck, 0.2f, ground);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        if (!isGrounded) velocityY += gravity * 2f * Time.deltaTime;
        else if (isGrounded && velocityY < 0) velocityY = 0;
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        if (isGrounded && Input.GetButtonDown("Jump")) velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}