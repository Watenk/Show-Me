using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Gun currentGun;

    void Start()
    {
        InputManager.Instance.OnLeftMouseDown += Shoot;
    }

    private void Shoot()
    {
        currentGun.Shoot();
    }
}
