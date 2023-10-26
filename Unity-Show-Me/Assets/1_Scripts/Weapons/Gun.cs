using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour 
{
    public Bullet bullet;
    public GameObject bulletSpawn;

    public void Shoot()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        newBullet.Move(newBullet.transform.up, newBullet.speedMultiplier, ForceMode.Impulse);
    }
}