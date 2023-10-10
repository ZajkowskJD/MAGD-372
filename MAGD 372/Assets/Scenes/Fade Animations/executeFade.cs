using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class executeFade : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) anim.SetTrigger("execute");
    }
}
