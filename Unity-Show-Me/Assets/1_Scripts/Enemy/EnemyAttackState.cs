using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : BaseState
{
    public event Action OnAttackShoot;
    public event Action OnAttackUpdate;

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
            ShootTimer = GameSettings.Instance.EnemyShootDelay;
        }
    }

    public override void OnExit()
    {

    }
}
