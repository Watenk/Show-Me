using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    Bullet Bullet { get; }
    GameObject BulletSpawn { get; }

    void Shoot();
}
