using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float heavyMod = 4;
    [SerializeField] private float jumpForce = 15;

    private float airTime = 0;
    private bool grounded = false;
    private CharacterController controller;
    private Vector3 heightOffset = new Vector3(0, -1.05f, 0);
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {

        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 movementInput = xInput * transform.right + zInput * transform.forward;
        controller.Move(movementInput * speed * Time.deltaTime + Physics.gravity * Time.deltaTime * airTime * heavyMod);
    }

    private void FixedUpdate()
    {
        if(Physics.CheckSphere(transform.position + heightOffset, 0.4f, groundMask)) {
            grounded = true;
            airTime = Time.deltaTime;
            return;
        }
        grounded = false;
        airTime += Time.fixedDeltaTime;
    }
}
