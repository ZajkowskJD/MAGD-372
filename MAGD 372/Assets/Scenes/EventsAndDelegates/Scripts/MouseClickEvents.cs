using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickEvents : MonoBehaviour
{
    //fireactiveweapon
    public delegate void FireAction();
    public static event FireAction OnLMB;

    //swapactiveweapon
    public delegate void SwapAction();
    public static event SwapAction OnRMB;


    void Update() {
        if (Input.GetMouseButtonDown(0) && OnLMB != null) OnLMB();
        else if (Input.GetMouseButtonDown(1) && OnRMB != null) OnRMB();
    }
}
