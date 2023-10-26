using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public override void Awake()
    {
        base.Awake();
        speedMultiplier = GameSettings.Instance.NormalBulletSpeed;
    }
}
