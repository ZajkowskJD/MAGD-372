using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    private int activeWeapon = 0;
    private int hits = 0;
    private int misses = 0;
    [SerializeField] private GameObject[] gunModels;
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private TextMeshProUGUI missText;
    [SerializeField] private LayerMask hitmask;

    private void Start() {
        hitText.text = "Hits: " + hits;
        missText.text = "Misses: " + misses;
    }

    private void OnEnable() {
        MouseClickEvents.OnLMB += FirePistol;
        MouseClickEvents.OnRMB += SwapWeapons;
    }

    private void OnDisable() {
        MouseClickEvents.OnRMB -= SwapWeapons;
        switch(activeWeapon) {
            case 0: MouseClickEvents.OnLMB -= FirePistol;
                break;
            case 1: MouseClickEvents.OnRMB -= FireShotgun;
                break;
        }
    }

    private void FirePistol() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        CastShot(ray);
    }

    private void FireShotgun() {
        Vector3[] spread = { new Vector3(0.45f, 0.45f, 0f), new Vector3(0.45f, 0.55f, 0f), new Vector3(0.55f, 0.45f, 0f), new Vector3(0.55f, 0.55f, 0f) };
        foreach(Vector3 v in spread) {
            Ray ray = Camera.main.ViewportPointToRay(v);
            CastShot(ray);
        }
    }

    private void SwapWeapons() {
        Debug.Log("swapping");
        switch(activeWeapon) {
            case 0:
                gunModels[activeWeapon].SetActive(false);
                activeWeapon = 1;
                gunModels[activeWeapon].SetActive(true);
                MouseClickEvents.OnLMB -= FirePistol;
                MouseClickEvents.OnLMB += FireShotgun;
                break;
            case 1:
                gunModels[activeWeapon].SetActive(false);
                activeWeapon = 0;
                gunModels[activeWeapon].SetActive(true);
                MouseClickEvents.OnLMB -= FireShotgun;
                MouseClickEvents.OnLMB += FirePistol;
                break;
        }
    }

    private void CastShot(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 20f, hitmask)) {
            Debug.Log("hit " + hit.collider.name);
            hits++;
            hitText.text = "Hits: " + hits;
        }
        else {
            misses++;
            missText.text = "Misses: " + misses;
        }
    }
}
