using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IShootable
{
    public Bullet Bullet { get { return bullet; } }
    public GameObject BulletSpawn { get { return bulletSpawn; } }

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private GameObject bulletSpawn;

    public void Shoot()
    {
        Bullet newBullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        newBullet.Move(newBullet.transform.up, newBullet.speedMultiplier, ForceMode.Impulse);
    }
}