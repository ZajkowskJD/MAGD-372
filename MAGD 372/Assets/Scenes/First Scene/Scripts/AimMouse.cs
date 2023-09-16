using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMouse : MonoBehaviour
{
    [SerializeField] private float mouseSens = 100;
    [SerializeField] private Transform charTransform;

    private float xRotation = 0;
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        charTransform.Rotate(Vector3.up * mouseX);
    }
}
