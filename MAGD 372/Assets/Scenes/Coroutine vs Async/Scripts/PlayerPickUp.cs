using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float pickupRange;
    [SerializeField] private Transform lockPoint;
    [SerializeField] private float launchForce = 20f;

    private bool held = false;
    private DodgeballController heldBall = null;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (heldBall != null) {
                heldBall.DisableLock();
                heldBall.Launch(Camera.main.transform.forward, launchForce);
            }
            TryPickup();
        }
    }

    private void TryPickup()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if(Physics.Raycast(ray, out hit, pickupRange, mask))
        {
            Debug.Log(hit.collider.name);
            hit.collider.gameObject.GetComponent<DodgeballController>().LockToPoint(lockPoint);
            heldBall = hit.collider.gameObject.GetComponent<DodgeballController>();
        }
    }
}
