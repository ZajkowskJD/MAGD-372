using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DodgeballController : MonoBehaviour
{
    private Rigidbody rb;
    private bool locked = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "unlocked";
    }

    public async void LockToPoint(Transform point)
    {
        locked = true;
        gameObject.tag = "locked";
        while (locked)
        {
            transform.position = point.position;
            await Task.Yield();
        }
    }

    public void DisableLock()
    {
        locked = false;
        gameObject.tag = "unlocked";
    }

    public void Launch(Vector3 direction, float magnitude)
    {
        rb.velocity = direction * magnitude;
    }
}
