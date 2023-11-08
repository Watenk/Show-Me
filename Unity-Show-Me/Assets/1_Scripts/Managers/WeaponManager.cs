using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Gun currentGun;

    public event Action OnShoot;

    void Start()
    {
        InputManager.Instance.OnLeftMouseDown += Shoot;
    }

    private void Shoot()
    {
        currentGun.Shoot();
        OnShoot();
    }
}
