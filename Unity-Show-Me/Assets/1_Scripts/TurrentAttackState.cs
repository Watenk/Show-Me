using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : BaseState
{
    public event Action OnAttackUpdate;
    public event Action OnAttackShoot;

    private float ShootTimer;

    public override void OnStart()
    {

    }

    public override void OnUPS()
    {
        OnAttackUpdate();

        //Shoot
        ShootTimer -= Time.deltaTime;
        if (ShootTimer <= 0)
        {
            OnAttackShoot();
            ShootTimer = GameSettings.Instance.TurretShootDelay;
        }
    }

    public override void OnExit()
    {

    }
}
